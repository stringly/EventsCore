using AutoMapper;
using EventsCore.Application.Common.Mappings;
using EventsCore.Domain.Entities;

namespace EventsCore.Application.Ranks.Queries.GetRankList
{
    /// <summary>
    /// Data Transfer Class for the <see cref="Rank"></see> entity
    /// </summary>
    public class RankDto : IMapFrom<Rank>
    {
        /// <summary>
        /// The Id of the entity
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Full Name of the Entity
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// The abbreviation for the Entity
        /// </summary>
        public string Abbreviation { get; set; }
        /// <summary>
        /// Creates a mapping profile
        /// </summary>
        /// <param name="profile">The <see cref="Profile"></see></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Rank, RankDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FullName))
                .ForMember(d => d.Abbreviation, opt => opt.MapFrom(s => s.Abbreviation));
        }
    }
}
