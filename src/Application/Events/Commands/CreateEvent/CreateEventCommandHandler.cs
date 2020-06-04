using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Common.Interfaces;
using EventsCore.Domain.Common;
using EventsCore.Domain.Entities;
using EventsCore.Domain.Exceptions.ValueObjects;
using EventsCore.Domain.ValueObjects;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, int>
    {
        private readonly IEventsCoreDbContext _context;
        private readonly IDateTime _dateTime;

        public CreateEventCommandHandler(IEventsCoreDbContext context, IDateTime dateTime)
        {
            _context = context;
            _dateTime = dateTime;
        }

        public async Task<int> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var type = _context.EventTypes.Find(request.EventTypeId);
            if (type == null)
            {
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(Event.EventTypeId), $"No Event Type with Id: \"{request.EventTypeId}\" was found.") });
            }
            EventSeries series = null;
            if (request.EventSeriesId.HasValue)
            {
                series = _context.EventSeries.Find(request.EventSeriesId);
                if (series == null)
                {
                    throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(Event.EventSeriesId), $"No Event Series with Id: \"{request.EventTypeId}\" was found.") });
                }
            }
            EventDates dates;
            try
            {
                dates = new EventDates(request.StartDate, request.EndDate, request.RegStartDate, request.RegEndDate, _dateTime);
            }
            catch (EventDatesInvalidException e)
            {
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(Event.Dates), e.Message) });
            }
            EventRegistrationRules rules;
            try
            {
                if(!request.MinRegsCount.HasValue && !request.MaxStandbyCount.HasValue)
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
            }
            catch (EventRegistrationRulesArgumentException e)
            {
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(Event.Rules), e.Message) });
            }
            try
            {
                Event entity;
                if(series != null)
                {
                    entity = new Event(request.Title, request.Description, dates, rules, type.Id, series.Id);
                }
                else
                {
                    entity = new Event(request.Title, request.Description, dates, rules, type.Id);
                }
                await _context.Events.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.Id;

            }
            catch(Exception e)
            {
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(Event), e.Message) });
            }
        }
    }
}
