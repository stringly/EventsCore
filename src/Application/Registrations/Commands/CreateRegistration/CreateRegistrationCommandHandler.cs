using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Common.Interfaces;
using EventsCore.Common;
using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.Registrations.Commands.CreateRegistration
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"></see> that handles requests to create a <see cref="Domain.Entities.Registration"></see>
    /// </summary>
    public class CreateRegistrationCommandHandler : IRequestHandler<CreateRegistrationCommand, int>
    {
        private readonly IEventsCoreDbContext _context;
        private readonly IDateTime _dateTime;
        /// <summary>
        /// Creates a new instance of the handler
        /// </summary>
        /// <param name="context">An implementation of <see cref="IEventsCoreDbContext"></see></param>
        /// <param name="dateTime">An implementation of <see cref="IDateTime"></see></param>
        public CreateRegistrationCommandHandler(IEventsCoreDbContext context, IDateTime dateTime)
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
        /// Throw when the Event is not accepting registrations
        /// </exception>
        public async Task<int> Handle(CreateRegistrationCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Events.Find(request.EventId);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Event), request.EventId);
            }
            var user = _context.Users.Find(request.UserId);
            if (user == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.User), request.UserId);
            }
            if (!entity.IsAcceptingRegistrations(_dateTime))
            {
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure(nameof(entity.IsAcceptingRegistrations), $"Cannot create registration: The Event is not accepting registrations.") });
            }
            else
            {
                entity.RegisterUser(user.Id, user.DisplayName, user.Email, user.ContactNumber, _dateTime);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
        }
    }
}
