using AutoMapper;
using EventsCore.Application.Common.Mappings;
using EventsCore.Domain.Entities;

namespace EventsCore.Application.EventTypes.Queries.GetEventTypesList
{
    /// <summary>
    /// Data Transfer Class for the <see cref="EventType"></see> entity
    /// </summary>
    public class EventTypeDto : IMapFrom<EventType>
    {
        /// <summary>
        /// The entity Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The entity Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Creates a mapping profile
        /// </summary>
        /// <param name="profile">The <see cref="Profile"></see></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<EventType, EventTypeDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
        }
    }
}
