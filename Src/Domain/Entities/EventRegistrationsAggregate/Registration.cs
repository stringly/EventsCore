using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.EventRegistrationsAggregate;
using System;

namespace EventsCore.Domain.Entities.EventRegistrationsAggregate
{
    /// <summary>
    /// Entity that represents a User Registration for an Event
    /// </summary>
    public class Registration : IEntity
    {
        /// <summary>
        /// Parameterless constructor for EF
        /// </summary>
        private Registration() { }
        /// <summary>
        /// Creates a new Registration for an Event with the given parameters
        /// </summary>
        /// <remarks>
        /// The Registration Entity exists as a part of the <see cref="EventRegistrations">EventRegistrations</see> aggregate root. Registrations can only be created/update/removed from the aggregate root.
        /// </remarks>
        /// <param name="userId">An integer Id of the User associated with the registration</param>
        /// <param name="userName">A string containing the User's display name.</param>
        /// <param name="email">A string containing the User's email address.</param>
        /// <param name="contact">A string containing the User's primary contact number.</param>
        /// <exception cref="EventRegistrationAggregateArgumentException">
        /// Thrown when:
        /// <list type="bullet">
        /// <item>The provided userId parameter is 0 or out of range.</item>
        /// <item>The provided userName parameter is empty/whitespace.</item>
        /// <item>The provided email parameter is empty/whitespace.</item>
        /// </list>
        /// </exception>
        public Registration(int userId, string userName, string email, string contact)
        {
            UserId = userId != 0 ? userId : throw new EventRegistrationAggregateArgumentException("Cannot create Event Registration: Invalid user Id", nameof(userId));
            UserName = !string.IsNullOrWhiteSpace(userName) ? userName : throw new EventRegistrationAggregateArgumentException("Cannot create Event Registration: user name cannot be null/empty string", nameof(userName));
            Email = !string.IsNullOrWhiteSpace(email) ? email : throw new EventRegistrationAggregateArgumentException("Cannot create Event Registration: user email cannot be null/empty string", nameof(email));
            Contact = contact;
            Status = RegistrationStatus.Pending;
            Registered = DateTime.Now;
            StatusChanged = DateTime.Now;
        }
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string Contact { get; private set; }
        public RegistrationStatus Status { get; private set; }
        public DateTime Registered { get; private set; }
        public DateTime StatusChanged { get; private set; }

        public void UpdateStatusAccepted()
        {
            Status = RegistrationStatus.Accepted;
            StatusChanged = DateTime.Now;
        }
        public void UpdateStatusPending()
        {
            Status = RegistrationStatus.Pending;
            StatusChanged = DateTime.Now;
        }
        public void UpdateStatusStandby()
        {
            Status = RegistrationStatus.Standby;
            StatusChanged = DateTime.Now;
        }
        public void UpdateStatusRejected()
        {
            Status = RegistrationStatus.Rejected;
            StatusChanged = DateTime.Now;
        }
    }
}
