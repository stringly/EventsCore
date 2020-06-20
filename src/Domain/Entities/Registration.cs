using EventsCore.Common;
using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.Registration;
using System;

namespace EventsCore.Domain.Entities
{
    /// <summary>
    /// Entity that represents a User Registration for an Event
    /// </summary>
    public class Registration : BaseEntity
    {
        /// <summary>
        /// Parameterless constructor for EF
        /// </summary>
        private Registration() { }
        /// <summary>
        /// Creates a new Registration for an Event with the given parameters
        /// </summary>
        /// <remarks>
        /// The Registration Entity exists as a part of the <see cref="Event">EventRegistrations</see> aggregate root. Registrations can only be created/update/removed from the aggregate root.
        /// Registrations will be created with the default status of "Pending."
        /// </remarks>
        /// <param name="userId">An integer Id of the User associated with the registration</param>
        /// <param name="userName">A string containing the User's display name.</param>
        /// <param name="email">A string containing the User's email address.</param>
        /// <param name="contact">A string containing the User's primary contact number.</param>
        /// <param name="dateTime">An implementation if <see cref="IDateTime"></see> used to obtain the system time.</param>
        /// <exception cref="RegistrationArgumentException">
        /// Thrown when:
        /// <list type="bullet">
        /// <item><description>The provided userId parameter is 0 or out of range.</description></item>
        /// <item><description>The provided userName parameter is empty/whitespace.</description></item>
        /// <item><description>The provided email parameter is empty/whitespace.</description></item>
        /// </list>
        /// </exception>
        public Registration(int userId, string userName, string email, string contact, IDateTime dateTime)
        {
            _dateTime = dateTime;
            UserId = userId > 0 ? userId : throw new RegistrationArgumentException("Cannot create Event Registration: Invalid user Id", nameof(userId));
            UserName = !string.IsNullOrWhiteSpace(userName) ? userName : throw new RegistrationArgumentException("Cannot create Event Registration: user name cannot be null/empty string", nameof(userName));
            Email = !string.IsNullOrWhiteSpace(email) ? email : throw new RegistrationArgumentException("Cannot create Event Registration: user email cannot be null/empty string", nameof(email));
            Contact = contact;
            Status = RegistrationStatus.Pending;
            Registered = _dateTime.Now;
            StatusChanged = _dateTime.Now;
        }
        private readonly IDateTime _dateTime;
        /// <summary>
        /// The Id of the <see cref="Event"></see> associated with this registration.
        /// </summary>
        public int EventId { get; private set;}
        /// <summary>
        /// The UserId of the User associated with this Registration instance.
        /// </summary>
        public int UserId { get; private set; }
        /// <summary>
        /// The Display name of the User associated with this Registration instance.
        /// </summary>
        public string UserName { get; private set; }
        /// <summary>
        /// The Email address of the User associated with this Registration instance.
        /// </summary>
        public string Email { get; private set; }
        /// <summary>
        /// The contact number of the User associated with this Registration instance.
        /// </summary>
        public string Contact { get; private set; }
        /// <summary>
        /// The current <see cref="RegistrationStatus"></see> of this Registration instance.
        /// </summary>
        public RegistrationStatus Status { get; private set; }
        /// <summary>
        /// Timestamp for when this Registration instance was created.
        /// </summary>
        public DateTime Registered { get; private set; }
        /// <summary>
        /// Timestamp for when this Registration's <see cref="RegistrationStatus"></see> was last changed.
        /// </summary>
        public DateTime StatusChanged { get; private set; }
        /// <summary>
        /// Method that changes the <see cref="RegistrationStatus"></see> of this instance to <see cref="RegistrationStatus.Accepted"></see>
        /// </summary>
        /// <remarks>
        /// This method will also update the timestamp of <see cref="StatusChanged"></see>
        /// </remarks>
        public void UpdateStatusAccepted()
        {
            Status = RegistrationStatus.Accepted;
            StatusChanged = _dateTime.Now;
        }
        /// <summary>
        /// Method that changes the <see cref="RegistrationStatus"></see> of this instance to <see cref="RegistrationStatus.Pending"></see>
        /// </summary>
        /// <remarks>
        /// This method will also update the timestamp of <see cref="StatusChanged"></see>
        /// </remarks>
        public void UpdateStatusPending()
        {
            Status = RegistrationStatus.Pending;
            StatusChanged = _dateTime.Now;
        }
        /// <summary>
        /// Method that changes the <see cref="RegistrationStatus"></see> of this instance to <see cref="RegistrationStatus.Standby"></see>
        /// </summary>
        /// <remarks>
        /// This method will also update the timestamp of <see cref="StatusChanged"></see>
        /// </remarks>
        public void UpdateStatusStandby()
        {
            Status = RegistrationStatus.Standby;
            StatusChanged = _dateTime.Now;
        }
        /// <summary>
        /// Method that changes the <see cref="RegistrationStatus"></see> of this instance to <see cref="RegistrationStatus.Rejected"></see>
        /// </summary>
        /// <remarks>
        /// This method will also update the timestamp of <see cref="StatusChanged"></see>
        /// </remarks>
        public void UpdateStatusRejected()
        {
            Status = RegistrationStatus.Rejected;
            StatusChanged = _dateTime.Now;
        }
    }
    /// <summary>
    /// Enumeration representing the possible statuses for a <see cref="Registration"></see>
    /// </summary>
    public enum RegistrationStatus
    {
        /// <summary>
        /// Used when a registration is pending
        /// </summary>
        Pending,
        /// <summary>
        /// Used when a registration is accepted
        /// </summary>
        Accepted,
        /// <summary>
        /// Used when a registration is placed on standby
        /// </summary>        
        Standby,
        /// <summary>
        /// Used when a registration is rejected
        /// </summary>
        Rejected
    }
}
