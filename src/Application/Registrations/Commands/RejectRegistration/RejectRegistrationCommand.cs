using MediatR;

namespace EventsCore.Application.Registrations.Commands.RejectRegistration
{
    /// <summary>
    /// Implementation of <see cref="IRequest"></see> that updates the status of a <see cref="Domain.Entities.Registration"></see> to <see cref="Domain.Entities.RegistrationStatus.Rejected"/>
    /// </summary>
    public class RejectRegistrationCommand : IRequest<int>
    {
        /// <summary>
        /// The Id of the Event to which the Registration belongs.
        /// </summary>
        public int EventId { get; set; }
        /// <summary>
        /// The Id of the Registration to update.
        /// </summary>
        public int RegistrationId { get; set; }
    }
}
