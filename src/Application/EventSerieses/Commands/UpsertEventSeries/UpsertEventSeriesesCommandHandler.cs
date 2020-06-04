using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Common.Interfaces;
using EventsCore.Domain.Entities;
using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.EventSerieses.Commands.UpsertEventSeries
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"></see> that handles requests to update/create an Event Series
    /// </summary>
    public class UpsertEventSeriesesCommandHandler : IRequestHandler<UpsertEventSeriesesCommand, int>
    {
        private readonly IEventsCoreDbContext _context;
        /// <summary>
        /// Creates a new instance of the handler
        /// </summary>
        /// <param name="context">An implementation of <see cref="IEventsCoreDbContext"></see></param>
        public UpsertEventSeriesesCommandHandler(IEventsCoreDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Handles the request to insert or update the <see cref="EventSeries"></see>
        /// </summary>
        /// <param name="request">The command</param>
        /// <param name="cancellationToken">The cancellationToken</param>
        /// <returns>A <see cref="Task"> containing the Integer Id of the upserted entity.</see></returns>
        /// <exception cref="ValidationException">Throw when the "Title" property of the request parameter is in use by another <see cref="EventSeries"></see></exception>
        /// <exception cref="NotFoundException">Throw when the "Id" property of the request parameter is present, but does not match the Id of any existing <see cref="EventSeries"></see></exception>
        public async Task<int> Handle(UpsertEventSeriesesCommand request, CancellationToken cancellationToken)
        {
            EventSeries entity;
            if (request.Id.HasValue)
            {
                // we are updating an existing Event Series
                var titleIsTaken = _context.EventSeries.Any(x => x.Title == request.Title && x.Id != request.Id);
                if (titleIsTaken)
                {
                    throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(EventSeries.Title), $"The title \"{request.Title}\" is already in use.")});
                }
                entity = await _context.EventSeries.FindAsync(request.Id);
                if (entity == null)
                {
                    throw new NotFoundException(nameof(EventSeries), request.Id);
                }
                entity.UpdateTitle(request.Title);
                entity.UpdateDescription(request.Description);
            }
            else
            {
                // we are creating a new Event Series
                var titleIsTaken = _context.EventSeries.Any(x => x.Title == request.Title);
                if (titleIsTaken)
                {
                    throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(EventSeries.Title), $"The title \"{request.Title}\" is already in use.")});
                }
                entity = new EventSeries(request.Title, request.Description);
                await _context.EventSeries.AddAsync(entity);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
