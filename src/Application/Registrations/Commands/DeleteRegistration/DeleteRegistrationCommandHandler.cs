using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Common.Interfaces;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.Registrations.Commands.DeleteRegistration
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"></see> that handles requests to delete a <see cref="Domain.Entities.Registration"></see>
    /// </summary>
    public class DeleteRegistrationCommandHandler : IRequestHandler<DeleteRegistrationCommand, int>
    {        
        private readonly IEventsCoreDbContext _context;
        /// <summary>
        /// Creates a new instance of the handler
        /// </summary>
        /// <param name="context">An implementation of <see cref="IEventsCoreDbContext"></see></param>        
        public DeleteRegistrationCommandHandler(IEventsCoreDbContext context)
        {
            _context = context;            
        }
        /// <summary>
        /// Handles the request to delete the <see cref="Domain.Entities.Registration"></see>
        /// </summary>
        /// <param name="request">The command</param>
        /// <param name="cancellationToken">The cancellationToken</param>
        /// <returns>A <see cref="Task"> containing the Integer Id of the newly inserted entity.</see></returns>
        /// <exception cref="ValidationException">
        /// Throw when the Registration could not be deleted in the Event internal method.
        /// </exception>
        public async Task<int> Handle(DeleteRegistrationCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Events.Find(request.EventId);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Event), request.EventId);
            }
            try
            {
                entity.DeleteRegistrationByRegistrationId(request.RegistrationId);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(entity.DeleteRegistrationByRegistrationId), $"Cannot delete registration: {ex.Message}") });
            }
        }
    }
}
