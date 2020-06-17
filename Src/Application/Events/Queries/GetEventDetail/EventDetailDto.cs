using AutoMapper;
using EventsCore.Application.Common.Mappings;
using EventsCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsCore.Application.Events.Queries.GetEventDetail
{
    public class EventDetailDto : IMapFrom<Event>
    {
        /// <summary>
        /// The entity Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Event's title.
        /// </summary>
        public string Title { get; set;  }
        /// <summary>
        /// The Event's <see cref="Domain.Entities.EventType"/> Id.
        /// </summary>
        public int EventTypeId { get; set; }
        /// <summary>
        /// The Event's <see cref="Domain.Entities.EventType"/> name.
        /// </summary>
        public string EventTypeTitle { get; set; }
        /// <summary>
        /// The Event's Series Id, if any.
        /// </summary>
        public int? EventSeriesId { get; set; }
        /// <summary>
        /// The Event's Series Name, if any
        /// </summary>
        public string EventSeriesName { get;set;}

        /// <summary>
        /// The Event's Timeframe
        /// </summary>
        public string Timeframe { get; set; }
        /// <summary>
        /// The Event's Registration Period Timeframe
        /// </summary>
        public string RegistrationPeriod { get; set; }
        /// <summary>
        /// The Event's location
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// The Event's description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Creates a mapping between the base <see cref="Event"/> and the Dto <see cref="EventDetailDto"/>
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event, EventDetailDto>()
                .ForMember(e => e.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(e => e.Title, opt => opt.MapFrom(s => s.Title))
                .ForMember(e => e.EventTypeId, opt => opt.MapFrom(s => s.EventTypeId))
                .ForMember(e => e.EventTypeTitle, opt => opt.MapFrom(s => s.EventType.Name))
                .ForMember(e => e.EventSeriesId, opt => opt.MapFrom(s => s.EventSeriesId))
                .ForMember(e => e.EventSeriesName, opt => opt.MapFrom(s => s.EventSeries == null ? "None" : s.EventSeries.Title))
                .ForMember(e => e.Timeframe, opt => opt.MapFrom(s => ($"{s.StartDate.ToString()} - {s.EndDate.ToString()} ")))
                .ForMember(e => e.RegistrationPeriod, opt => opt.MapFrom(s => ($"{s.RegistrationStartDate.ToString()} = {s.RegistrationEndDate.ToString()}")))
                .ForMember(e => e.Location, opt => opt.MapFrom(s => s.Location))
                .ForMember(e => e.Description, opt => opt.MapFrom(s => s.Description));
        }
    }
}
