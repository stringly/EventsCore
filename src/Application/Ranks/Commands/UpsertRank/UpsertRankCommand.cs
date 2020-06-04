using EventsCore.Domain.Entities;
using MediatR;

namespace EventsCore.Application.Ranks.Commands.UpsertRank
{
    /// <summary>
    /// Implementation of <see cref="IRequest"></see> that creates/updates a <see cref="Rank"></see>
    /// </summary>
    public class UpsertRankCommand : IRequest<int>
    {
        /// <summary>
        /// The Id of the <see cref="Rank"></see> being upserted. Will be null for a new Rank
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// The Abbreviation for the Rank
        /// </summary>
        public string Abbrev { get; set; }
        /// <summary>
        /// The Full name of the <see cref="Rank"></see> being upserted
        /// </summary>
        public string FullName { get; set; }
    }
}
