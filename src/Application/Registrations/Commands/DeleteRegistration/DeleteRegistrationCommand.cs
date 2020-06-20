using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsCore.Application.Registrations.Commands.DeleteRegistration
{
    /// <summary>
    /// Implementation of <see cref="IRequest"></see> that deletes a <see cref="Domain.Entities.Registration"/>
    /// </summary>
    public class DeleteRegistrationCommand : IRequest<int>
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
