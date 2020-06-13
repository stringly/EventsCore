using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Common.Interfaces;
using EventsCore.Domain.Common;
using EventsCore.Domain.Entities;
using EventsCore.Domain.ValueObjects;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.Events.Commands.CreateEvent
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"></see> that handles requests to create an <see cref="Event"></see>
    /// </summary>
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, int>
    {
        private readonly IEventsCoreDbContext _context;
        private readonly IDateTime _dateTime;
        /// <summary>
        /// Creates a new instance of the handler
        /// </summary>
        /// <param name="context">An implementation of <see cref="IEventsCoreDbContext"></see></param>
        /// <param name="dateTime">An implementation of <see cref="IDateTime"></see></param>
        public CreateEventCommandHandler(IEventsCoreDbContext context, IDateTime dateTime)
        {
            _context = context;
            _dateTime = dateTime;
        }
        /// <summary>
        /// Handles the request to insert the <see cref="Event"></see>
        /// </summary>
        /// <param name="request">The command</param>
        /// <param name="cancellationToken">The cancellationToken</param>
        /// <returns>A <see cref="Task"> containing the Integer Id of the newly inserted entity.</see></returns>
        /// <exception cref="ValidationException">
        /// Throw when:
        /// <list type="bullet">
        /// <item><description>the EventTypeId property of the request parameter does not match any existing <see cref="EventType"></see></description></item>
        /// <item><description>the EventSeriesId property of the request parameter is present but does not match any existing <see cref="EventSeries"></see></description></item>
        /// <item><description>one of the Dates used to construct the <see cref="EventDates"></see> value object caused an error in that object's constructor.</description></item>
        /// <item><description>an error was thrown in the constructor of the <see cref="EventRegistrationRules"></see> value object, likely because of a bad registration count value.</description></item>
        /// <item><description>an error was thrown in the constructor of the <see cref="Event"></see> entity object, likely because of a bad parameter that was not caught by validation.</description></item>
        /// </list>
        /// </exception>
        public async Task<int> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            // Attempt to find the Event Type that matches request.EventTypeId
            var type = _context.EventTypes.Find(request.EventTypeId);
            if (type == null)
            {
                // throw if no EventType was found
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(Event.EventTypeId), $"No Event Type with Id: \"{request.EventTypeId}\" was found.") });
            }
            // Attempt to find the Event Series that matches request.EventSeriesId
            EventSeries series = null;
            if (request.EventSeriesId.HasValue) // request.EventSeriesId is an optional parameter, so check if it is present
            {
                // Attempt to locate the matching series
                series = _context.EventSeries.Find(request.EventSeriesId);
                if (series == null)
                {
                    // throw if no matching series was found
                    throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(Event.EventSeriesId), $"No Event Series with Id: \"{request.EventSeriesId}\" was found.") });
                }
            }
            try
            {
                Event entity = new Event(
                    request.Title,
                    request.Description,
                    request.EventTypeId,
                    request.EventSeriesId,
                    request.StartDate,
                    request.EndDate,
                    request.RegStartDate,
                    request.RegEndDate,
                    request.MaxRegsCount,
                    request.MinRegsCount,
                    request.MaxStandbyCount,
                    request.Street,
                    request.Suite,
                    request.City,
                    request.State,
                    request.Zip
                    );
                await _context.Events.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.Id;

            }
            catch(Exception e)
            {
                // throw if the Event constructor threw an error, which is likely because a bad parameter made it through validation 
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(Event), e.Message) });
            }
        }
    }
}
