using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Common.Interfaces;
using EventsCore.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.EventSerieses.Commands.DeleteEventSeries
{
    /// <summary>
    /// An implementation of <see cref="IRequestHandler{TRequest}"></see> that deletes an <see cref="EventSeries"></see>
    /// </summary>
    public class DeleteEventSeriesesCommandHandler : IRequestHandler<DeleteEventSeriesesCommand>
    {
        private readonly IEventsCoreDbContext _context;
        /// <summary>
        /// Creates a new instance of the Handler
        /// </summary>
        /// <param name="context">An implementation of <see cref="IEventsCoreDbContext"></see></param>
        public DeleteEventSeriesesCommandHandler(IEventsCoreDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Handles the request
        /// </summary>
        /// <param name="request">A <see cref="DeleteEventSeriesesCommand"></see> request object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"></see></param>        
        /// <returns>A <see cref="Unit"></see> containing the results of the operation.</returns>
        /// <exception cref="NotFoundException">Thrown when no <see cref="EventSeries"></see> with the request Id parameter was found.</exception>
        /// <exception cref="DeleteFailureException">Thrown when the <see cref="EventSeries"></see> being deleted has <see cref="Event"></see>s.</exception>
        public async Task<Unit> Handle(DeleteEventSeriesesCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.EventSeries.FindAsync(request.Id);
            if(entity == null)
            {
                throw new NotFoundException(nameof(EventSeries), request.Id);
            }
            var hasEvents = _context.Events.Any(x => x.EventSeriesId == request.Id);
            if (hasEvents)
            {
                throw new DeleteFailureException(nameof(EventSeries), request.Id, "There are Events associated with this Event Series.");
            }
            _context.EventSeries.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
