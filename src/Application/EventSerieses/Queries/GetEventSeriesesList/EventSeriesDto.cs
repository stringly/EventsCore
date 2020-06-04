using AutoMapper;
using EventsCore.Application.Common.Mappings;
using EventsCore.Domain.Entities;

namespace EventsCore.Application.EventSerieses.Queries.GetEventSeriesesList
{
    /// <summary>
    /// Data Transfer Class for the <see cref="EventSeries"></see> entity
    /// </summary>
    public class EventSeriesDto : IMapFrom<EventSeries>
    {
        /// <summary>
        /// The entity Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The entity's title
        /// </summary>
        public string Title { get; set; }        
        
        /// <summary>
        /// Creates a mapping between the base <see cref="EventSeries"></see> and the Dto <see cref="EventSeriesDto"></see>
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<EventSeries, EventSeriesDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title));
        }

    }
}
