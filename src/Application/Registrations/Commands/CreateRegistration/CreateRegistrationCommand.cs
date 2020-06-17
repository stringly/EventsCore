using MediatR;

namespace EventsCore.Application.Registrations.Commands.CreateRegistration
{
    /// <summary>
    /// Implementation of <see cref="IRequest"></see> that creates a <see cref="Domain.Entities.Registration"></see>
    /// </summary>
    public class CreateRegistrationCommand : IRequest<int>
    {
        /// <summary>
        /// The Id of the Event for which the registration is being created.
        /// </summary>
        public int EventId { get;set;}
        /// <summary>
        /// The Id of the User for which we are creating a registration.
        /// </summary>
        public int UserId { get;set;}
        
    }
}
