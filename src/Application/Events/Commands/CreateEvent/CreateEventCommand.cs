using MediatR;
using System;

namespace EventsCore.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RegStartDate { get; set; }
        public DateTime RegEndDate { get; set; }
        public int MaxRegsCount { get; set; }
        public int? MinRegsCount { get; set; }
        public int? MaxStandbyCount { get; set; }
        public int EventTypeId { get; set; }
        public int? EventSeriesId { get; set; }
    }
}
