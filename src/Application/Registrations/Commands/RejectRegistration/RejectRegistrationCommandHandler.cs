using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Common.Interfaces;
using EventsCore.Domain.Common;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.Registrations.Commands.RejectRegistration
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"></see> that handles requests to reject a <see cref="Domain.Entities.Registration"></see>
    /// </summary>
    public class RejectRegistrationCommandHandler : IRequestHandler<RejectRegistrationCommand, int>
    {
        private readonly IEventsCoreDbContext _context;
        private readonly IDateTime _dateTime;
        /// <summary>
        /// Creates a new instance of the handler
        /// </summary>
        /// <param name="context">An implementation of <see cref="IEventsCoreDbContext"></see></param>
        /// <param name="dateTime">An implementation of <see cref="IDateTime"></see></param>
        public RejectRegistrationCommandHandler(IEventsCoreDbContext context, IDateTime dateTime)
        {
            _context = context;
            _dateTime = dateTime;
        }
        /// <summary>
        /// Handles the request to insert the <see cref="Domain.Entities.Registration"></see>
        /// </summary>
        /// <param name="request">The command</param>
        /// <param name="cancellationToken">The cancellationToken</param>
        /// <returns>A <see cref="Task"> containing the Integer Id of the newly inserted entity.</see></returns>
        /// <exception cref="ValidationException">
        /// Throw when the Registration status could not be changed in the Event internal method.
        /// </exception>
        public async Task<int> Handle(RejectRegistrationCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Events.Find(request.EventId);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Event), request.EventId);
            }
            try
            {
                entity.RejectRegistrationByRegistrationId(request.RegistrationId, _dateTime);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(entity.AcceptRegistrationByUserId), $"Cannot accept registration: {ex.Message}") });
            }
        }
    }
}
