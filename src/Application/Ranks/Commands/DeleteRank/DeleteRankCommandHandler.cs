using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Common.Interfaces;
using EventsCore.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.Ranks.Commands.DeleteRank
{
    /// <summary>
    /// Implements <see cref="IRequestHandler{TRequest}"></see> to Delete a Rank.
    /// </summary>
    public class DeleteRankCommandHandler : IRequestHandler<DeleteRankCommand>
    {
        private readonly IEventsCoreDbContext _context;
        /// <summary>
        /// Creates a new instance of the handler.
        /// </summary>
        /// <param name="context">An implementation of <see cref="IEventsCoreDbContext"></see></param>
        public DeleteRankCommandHandler(IEventsCoreDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Handles the request
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"></see></param>
        /// <returns>A <see cref="Unit"></see> containing the results of the operation.</returns>
        /// <exception cref="NotFoundException">Thrown when no <see cref="Rank"></see> with the request Id parameter was found.</exception>
        /// <exception cref="DeleteFailureException">Thrown when the <see cref="Rank"></see> being deleted has <see cref="User"></see>s.</exception>
        public async Task<Unit> Handle(DeleteRankCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Ranks
                .FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Rank), request.Id);
            }
            var hasUsers = _context.Users.Any(x => x.RankId == request.Id);
            if (hasUsers)
            {
                throw new DeleteFailureException(nameof(Rank), request.Id, "There are existing Users with this Rank.");
            }
            _context.Ranks.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
