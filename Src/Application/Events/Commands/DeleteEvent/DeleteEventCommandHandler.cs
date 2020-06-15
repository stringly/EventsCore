using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Common.Interfaces;
using EventsCore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.Events.Commands.DeleteEvent
{
    /// <summary>
    /// An implementation of <see cref="IRequestHandler{TRequest}"></see> that deletes an <see cref="Event"></see>
    /// </summary>
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
    {
        private readonly IEventsCoreDbContext _context;
        /// <summary>
        /// Creates a new instance of the Handler
        /// </summary>
        /// <param name="context">An implementation of <see cref="IEventsCoreDbContext"></see></param>
        public DeleteEventCommandHandler(IEventsCoreDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Handles the request
        /// </summary>
        /// <param name="request">A <see cref="DeleteEventCommand"></see> request object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"></see></param>        
        /// <returns>A <see cref="Unit"></see> containing the results of the operation.</returns>
        /// <exception cref="NotFoundException">Thrown when no <see cref="Event"></see> with the request Id parameter was found.</exception>
        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Events.FindAsync(request.Id);
            if(entity == null)
            {
                throw new NotFoundException(nameof(Event), request.Id);
            }
            var hasRegistrations = _context.EventRegistrations.Any(x => x.Id == entity.Id);
            if (hasRegistrations)
            {
                // TODO: Notify Registrants that event has been cancelled.
            }
            _context.Events.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
