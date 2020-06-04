using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Common.Interfaces;
using EventsCore.Domain.Entities;
using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.EventTypes.Commands.UpsertEventType
{
    /// <summary>
    /// Implements <see cref="IRequestHandler{TRequest, TResponse}"></see> to handle a request to update/insert an Event Type.
    /// </summary>
    public class UpsertEventTypeCommandHandler : IRequestHandler<UpsertEventTypeCommand, int>
    {
        private readonly IEventsCoreDbContext _context;
        /// <summary>
        /// Creates a new instance of the Handler
        /// </summary>
        /// <param name="context">An implementation of <see cref="IEventsCoreDbContext"></see></param>
        public UpsertEventTypeCommandHandler(IEventsCoreDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Handles the request to insert or update the <see cref="EventType"></see>
        /// </summary>
        /// <param name="request">The command</param>
        /// <param name="cancellationToken">The cancellationToken</param>
        /// <returns>A <see cref="Task"> containing the Integer Id of the upserted entity.</see></returns>
        /// <exception cref="ValidationException">Throw when the "Name" property of the request parameter is in use by another <see cref="EventType"></see></exception>
        /// <exception cref="NotFoundException">Throw when the "Id" property of the request parameter is present, but does not match the Id of any existing <see cref="EventType"></see></exception>
        public async Task<int> Handle(UpsertEventTypeCommand request, CancellationToken cancellationToken)
        {
            EventType entity;
            if (request.Id.HasValue)
            {
                var nameIsTaken = _context.EventTypes.Any(x => x.Name == request.Name && x.Id != request.Id);
                if (nameIsTaken)
                {
                    throw new ValidationException(new List<ValidationFailure>(){ new ValidationFailure(nameof(EventType.Name), $"The name \"{request.Name}\" is already in use.")} );
                }
                entity = await _context.EventTypes.FindAsync(request.Id.Value);
                if (entity == null)
                {
                    throw new NotFoundException(nameof(EventType), request.Id);
                }
                entity.UpdateName(request.Name);
            }
            else
            {
                var nameIsTaken = _context.EventTypes.Any(x => x.Name == request.Name);
                if (nameIsTaken)
                {
                    throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(EventType.Name), $"The name \"{request.Name}\" is already in use.") });
                }
                entity = new EventType(request.Name);
                await _context.EventTypes.AddAsync(entity);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
