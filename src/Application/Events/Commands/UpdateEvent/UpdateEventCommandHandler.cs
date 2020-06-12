using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Common.Interfaces;
using EventsCore.Domain.Common;
using EventsCore.Domain.Entities;
using EventsCore.Domain.Exceptions.Event;
using EventsCore.Domain.Exceptions.ValueObjects;
using EventsCore.Domain.ValueObjects;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.Events.Commands.UpdateEvent
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"></see> that handles requests to create an <see cref="Event"></see>
    /// </summary>
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, int>
    {
        private readonly IEventsCoreDbContext _context;
        private readonly IDateTime _dateTime;
        /// <summary>
        /// Creates a new instance of the handler
        /// </summary>
        /// <param name="context">An implementation of <see cref="IEventsCoreDbContext"></see></param>
        /// <param name="dateTime">An implementation of <see cref="IDateTime"></see></param>
        public UpdateEventCommandHandler(IEventsCoreDbContext context, IDateTime dateTime)
        {
            _context = context;
            _dateTime = dateTime;
        }
        /// <summary>
        /// Handles the request to update the <see cref="Event"></see>
        /// </summary>
        /// <param name="request">The command</param>
        /// <param name="cancellationToken">The cancellationToken</param>
        /// <returns>A <see cref="Task"> containing the Integer Id of entity being updated.</see></returns>
        /// <exception cref="ValidationException">
        /// Throw when:
        /// <list type="bullet">        /// 
        /// <item><description>the EventTypeId property of the request parameter does not match any existing <see cref="EventType"></see></description></item>
        /// <item><description>the EventSeriesId property of the request parameter is present but does not match any existing <see cref="EventSeries"></see></description></item>
        /// <item><description>one of the Dates used to construct the <see cref="EventDates"></see> value object caused an error in that object's constructor.</description></item>
        /// <item><description>an error was thrown in the constructor of the <see cref="EventRegistrationRules"></see> value object, likely because of a bad registration count value.</description></item>
        /// <item><description>an error was thrown in the constructor of the <see cref="Event"></see> entity object, likely because of a bad parameter that was not caught by validation.</description></item>
        /// </list>
        /// </exception>
        /// <exception cref="NotFoundException">Thrown when the Id parameter of the command does not match any existing entity.</exception>
        public async Task<int> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Events.Find(request.Id);
            if(entity == null) // ensure entity was found
            {
                throw new NotFoundException(nameof(Event), request.Id);
            }

            if (request.EventSeriesId != null)
            {
                var es = _context.EventSeries.Find(request.EventSeriesId);
                if (es == null)
                {
                    throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(Event.EventSeriesId), $"No Event Series with Id: \"{request.EventSeriesId}\" was found.") });
                }                
                entity.AddEventToSeries(es.Id);
            }
            else
            {
                entity.RemoveEventFromSeries();
            }

            if (request.EventTypeId != entity.EventTypeId)
            {
                var et = _context.EventTypes.Find(request.EventTypeId);
                if (et == null)
                {
                    throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(Event.EventSeriesId), $"No Event Series with Id: \"{request.EventSeriesId}\" was found.") });
                }
                entity.UpdateEventType(et.Id);
            }
            try
            {
                EventDates newDates = new EventDates(request.StartDate, request.EndDate, request.RegStartDate, request.RegEndDate, _dateTime);
                if (newDates != entity.Dates)
                {
                    entity.UpdateEventDates(newDates);
                }
            }
            catch (Exception ex)
            {
                // throw if an error occurred building the EventDates object, as there is likely an invalid date parameter present
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(Event.Dates), ex.Message) });
            }
            try
            {
                EventRegistrationRules rules;
                // hacky psuedo switch because I can't be bothered with a coalesce that calls the 3 param constructor with defaults... not that I didn't try
                if (!request.MinRegsCount.HasValue && !request.MaxStandbyCount.HasValue)
                {
                    rules = new EventRegistrationRules((uint)request.MaxRegsCount);
                }
                else if (request.MinRegsCount.HasValue && !request.MaxStandbyCount.HasValue)
                {
                    rules = new EventRegistrationRules((uint)request.MaxRegsCount, (uint)request.MinRegsCount);
                }
                else
                {
                    rules = new EventRegistrationRules((uint)request.MaxRegsCount, (uint)request.MinRegsCount, (uint)request.MaxStandbyCount);
                }
                entity.UpdateRegistrationRules(rules);
            }
            catch (Exception ex)
            {
                // throw if the RegistrationRules constructor threw an error, which is most likey because of an invalid parameter
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(Event.Rules), ex.Message) });
            }
            try
            {
                entity.UpdateTitle(request.Title);
                entity.UpdateDescription(request.Description);
            }
            catch (EventArgumentException ex)
            {
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(Event), ex.Message) });
            }
            
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
