using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.EventAggregate;
using System;

namespace EventsCore.Domain.Entities.EventAggregate
{
    public class Registration : IEntity, IAggregateRoot
    {
        private Registration() { }
        public Registration(Registrant registrant)
        {
            if(registrant == null)
            {
                throw new EventRegistrationInvalidRegistrantException("Cannot register user: parameter cannot be null.", nameof(registrant));
            }
            UserRegistered = registrant;
            TimeStamp = DateTime.Now;
            Status = RegistrationStatus.Pending;
            StatusChanged = DateTime.Now;
        }
        public Registration(Registrant registrant, RegistrationStatus status)
        {
            if (registrant == null)
            {
                throw new EventRegistrationInvalidRegistrantException("Cannot register user: parameter cannot be null.", nameof(registrant));
            }
            UserRegistered = registrant;
            TimeStamp = DateTime.Now;
            Status = Status;
            StatusChanged = DateTime.Now;
        }
        public int Id { get; private set; }
        public Registrant UserRegistered { get; private set;}
        public DateTime TimeStamp { get; private set; }
        public RegistrationStatus Status { get; private set; }
        public DateTime StatusChanged { get; private set; }

        public void UpdateStatusPending()
        {
            Status = RegistrationStatus.Pending;
        }
        public void UpdateStatusAccepted()
        {
            Status = RegistrationStatus.Accepted;
        }
        public void UpdateStatusStandby()
        {
            Status = RegistrationStatus.Standby;
        }
        public void UpdateStatusRejected()
        {
            Status = RegistrationStatus.Rejected;
        }
    }
    public enum RegistrationStatus
    {
        Pending,
        Accepted,
        Standby,
        Rejected
    }
}
