using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.EventRegistrationsAggregate;
using System;

namespace EventsCore.Domain.Entities.EventRegistrationsAggregate
{
    public class Registration : IEntity
    {
        private Registration() { }
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
