using AutoMapper;
using EventsCore.Application.Common.Mappings;
using EventsCore.Domain.Entities;
using System.Collections.Generic;

namespace EventsCore.Application.EventSerieses.Queries.GetEventSeriesesDetail
{
    /// <summary>
    /// Viewmodel class to show details for an <see cref="EventSeries"></see>
    /// </summary>
    public class EventSeriesDetailVm : IMapFrom<EventSeries>
    {
        /// <summary>
        /// The Id of the EventSeries
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Title of the EventSeries
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The Description of the EventSeries
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// A list of <see cref="EventSeriesEventDto"></see> for any <see cref="Event"></see>s in this Event Series
        /// </summary>
        public IList<EventSeriesEventDto> Events { get; set;}
        /// <summary>
        /// Creates a mapping profile between the <see cref="EventSeries"></see> and the <see cref="EventSeriesDetailVm"></see>
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<EventSeries, EventSeriesDetailVm>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.Events, opt => opt.Ignore());
        }
    }
}
