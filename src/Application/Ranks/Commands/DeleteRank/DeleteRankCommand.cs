using EventsCore.Domain.Entities;
using MediatR;

namespace EventsCore.Application.Ranks.Commands.DeleteRank
{
    /// <summary>
    /// An implementation of <see cref="IRequest"></see> that handles a request to remove a <see cref="Rank"></see>
    /// </summary>
    public class DeleteRankCommand : IRequest
    {
        /// <summary>
        /// The Id of the Rank to be removed
        /// </summary>
        public int Id { get; set; }
    }
}
