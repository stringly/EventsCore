using AutoMapper;
using EventsCore.Application.Common.Mappings;
using EventsCore.Domain.Entities;

namespace EventsCore.Application.EventSerieses.Queries.GetEventSeriesesDetail
{
    /// <summary>
    /// Data transfer class for an <see cref="Event"></see> that is part of an <see cref="EventSeriesDetailVm"/>
    /// </summary>
    public class EventSeriesEventDto : IMapFrom<Event>
    {
        /// <summary>
        /// The Id of the <see cref="Event"></see>
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Title of the <see cref="Event"></see> 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The StartDate of the <see cref="Event"></see>
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// Creates a mapping profile between the <see cref="Event"></see> and the <see cref="EventSeriesEventDto"></see>
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event, EventSeriesEventDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title))
                .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.Dates.StartDate.ToString()));
        }
    }
}
