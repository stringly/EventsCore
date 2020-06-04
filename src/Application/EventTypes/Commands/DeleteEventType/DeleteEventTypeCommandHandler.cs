using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Common.Interfaces;
using EventsCore.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.EventTypes.Commands.DeleteEventType
{
    /// <summary>
    /// Implements <see cref="IRequestHandler{TRequest}"></see> to Delete an Event Type.
    /// </summary>
    public class DeleteEventTypeCommandHandler : IRequestHandler<DeleteEventTypeCommand>
    {
        private readonly IEventsCoreDbContext _context;
        /// <summary>
        /// Creates a new instance of the handler.
        /// </summary>
        /// <param name="context">An implementation of <see cref="IEventsCoreDbContext"></see></param>
        public DeleteEventTypeCommandHandler(IEventsCoreDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Handles the request
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"></see></param>
        /// <returns>A <see cref="Unit"></see> containing the results of the operation.</returns>
        /// <exception cref="NotFoundException">Thrown when no <see cref="EventType"></see> with the request Id parameter was found.</exception>
        /// <exception cref="DeleteFailureException">Thrown when the <see cref="EventType"></see> being deleted has <see cref="Event"></see>s.</exception>
        public async Task<Unit> Handle(DeleteEventTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.EventTypes
                .FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(EventType), request.Id);
            }
            var hasEvents = _context.Events.Any(x => x.EventTypeId == entity.Id);
            if (hasEvents)
            {
                throw new DeleteFailureException(nameof(EventType), request.Id, "There are existing Events associated with this Event Type.");
            }
            _context.EventTypes.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;

        }
    }
}
