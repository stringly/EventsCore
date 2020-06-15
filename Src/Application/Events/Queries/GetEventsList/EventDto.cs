using AutoMapper;
using EventsCore.Application.Common.Mappings;
using EventsCore.Domain.Entities;

namespace EventsCore.Application.Events.Queries.GetEventsList
{
    /// <summary>
    /// Data Transfer Class for the <see cref="Event"></see> entity
    /// </summary>
    public class EventDto : IMapFrom<Event>
    {
        /// <summary>
        /// The entity Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Event's <see cref="EventType"/> name.
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// The Event's <see cref="EventSeries"/> name.
        /// </summary>
        public string SeriesName { get; set; }
        /// <summary>
        /// The Event's <see cref="EventSeries"/> id.
        /// </summary>
        public int? SeriesId { get; set; }
        /// <summary>
        /// The Event's title.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The Event's StartDate
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// The Event's EndDate
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// Creates a mapping between the base <see cref="Event"/> and the Dto <see cref="EventDto"/>
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event, EventDto>()
                .ForMember(e => e.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(e => e.TypeName, opt => opt.MapFrom(s => s.EventType.Name))
                .ForMember(e => e.SeriesName, opt => opt.MapFrom(s => s.EventSeries == null ? "None" : s.EventSeries.Title))
                .ForMember(e => e.SeriesId, opt => opt.MapFrom(s => s.EventSeriesId))
                .ForMember(e => e.Title, opt => opt.MapFrom(s => s.Title))
                .ForMember(e => e.StartDate, opt => opt.MapFrom(s => s.StartDate.ToString()))
                .ForMember(e => e.EndDate, opt => opt.MapFrom(s => s.EndDate.ToString()));
        }
    }
}
