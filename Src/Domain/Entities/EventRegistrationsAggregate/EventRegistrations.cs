using EventsCore.Domain.Common;
using EventsCore.Domain.Entities.ValueObjects;
using EventsCore.Domain.Exceptions.EventRegistrationsAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventsCore.Domain.Entities.EventRegistrationsAggregate
{
    public class EventRegistrations : IAggregateRoot
    {
        private EventRegistrations() { }
        public EventRegistrations(int eventId, EventDates eventDates, EventRegistrationRules registrationRules)
        {
            EventId = eventId != 0 ? eventId : throw new EventRegistrationAggregateArgumentException("Cannot create Event: eventId is invalid.", nameof(eventId));
            EventDates = eventDates ?? throw new EventRegistrationAggregateArgumentException("Cannot create Event: eventDates cannot be null.", nameof(eventDates));
            Rules = registrationRules ?? throw new EventRegistrationAggregateArgumentException("Cannot create Event: registrationRules cannot be null", nameof(registrationRules));
            _registrations = new List<Registration>();
        }
        public int EventId { get; private set; }
        public EventDates EventDates { get; private set; }
        /// <summary>
        /// The Event's Start Date, obtained from the Event.Dates ruleset.
        /// </summary>
        public DateTime StartDate => EventDates.StartDate;
        /// <summary>
        /// The Event's End Date, obtained from the Event.Dates ruleset.
        /// </summary>
        public DateTime EndDate => EventDates.EndDate;
        /// <summary>
        /// The Event's Registration Period Start Date, obtained from the Event.Dates ruleset.
        /// </summary>
        public DateTime RegistrationStartDate => EventDates.RegistrationStartDate;
        /// <summary>
        /// The Event's Registration Period End Date, obtained from the Event.Dates ruleset.
        /// </summary>
        public DateTime RegistrationEndDate => EventDates.RegistrationEndDate;
        public EventRegistrationRules Rules { get; private set; }
        /// <summary>
        /// The Maximum number of registrations, obtained from the Event.Rules ruleset.
        /// </summary>
        public uint MaxRegistrations => Rules.MaxRegistrations;
        private readonly List<Registration> _registrations;
        public IReadOnlyCollection<Registration> Registrations => _registrations;
        /// <summary>
        /// Count of "Accepted" registrations in the Event._registrations Collection
        /// </summary>
        public int CurrentAttendeesCount => _registrations.Count(x => x.Status == RegistrationStatus.Accepted);
        /// <summary>
        /// Count of "Standby" registrations in the Event._registrations collection
        /// </summary>
        public int CurrentStandbyCount => _registrations.Count(x => x.Status == RegistrationStatus.Standby);
        /// <summary>
        /// Returns true if the Event is accepting new Registrations
        /// </summary>
        /// <remarks>
        /// This property will test the following conditions:
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// Will return false if the Event's Start Date is in the Past.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Will return false if the number of "Accepted" registrations in the Event's collection is not less than the MaxRegistrations in the Event.Rules.
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        public bool IsAcceptingRegistrations {
            get {
                if (StartDate < DateTime.Now)
                {
                    return false;
                }
                else if (RegistrationStartDate > DateTime.Now || RegistrationEndDate < DateTime.Now)
                {
                    return false;
                }
                else if (MaxRegistrations >= CurrentAttendeesCount)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public void RegisterUser(int userId, string userName, string email, string contact)
        {
            if (!IsAcceptingRegistrations)
            {
                throw new EventRegistrationAggregateInvalidOperationException("Cannot add Registration: Event is not accepting Registrations");
            }
            if (userId == 0)
            {
                throw new EventRegistrationAggregateArgumentException("Cannot add Registration: UserId cannot be 0.", nameof(userId));
            }
            else if (string.IsNullOrEmpty(userName))
            {
                throw new EventRegistrationAggregateArgumentException("Cannot add Registration: UserName cannot be null/empty string.", nameof(userName));
            }
            else if (string.IsNullOrEmpty(email))
            {
                throw new EventRegistrationAggregateArgumentException("Cannot add Registration: User email cannot be null/empty string.", nameof(email));
            }
            else if (_registrations.Exists(x => x.UserId == userId))
            {
                throw new EventRegistrationAggregateInvalidOperationException("Cannot add Registration: User is already registered for this event.");
            }

            _registrations.Add(new Registration(userId, userName, email, contact));
        }
        public void UnregisterUserByUserId(int userId)
        {
            var registrationForUser = _registrations.FirstOrDefault(x => x.UserId == userId);

            if (registrationForUser == null)
            {
                throw new EventRegistrationAggregateArgumentException("Cannot remove registration: no registration for user id found.", nameof(userId));
            }
            _registrations.Remove(registrationForUser);
        }
        public void UnregisterUserByRegistrationId(int registrationId)
        {
            var registrationWithGivenId = _registrations.FirstOrDefault(x => x.Id == registrationId);
            if (registrationWithGivenId == null)
            {
                throw new EventRegistrationAggregateArgumentException("Cannot remove registration: no registration with the given id was found.", nameof(registrationId));
            }
            _registrations.Remove(registrationWithGivenId);
        }
        public void AcceptRegistrationById(int registrationId)
        {
            if (StartDate < DateTime.Now)
            {
                throw new EventRegistrationAggregateInvalidOperationException("Cannot accept registration: Event has expired.");
            }
            else if (CurrentAttendeesCount >= MaxRegistrations)
            {
                throw new EventRegistrationAggregateInvalidOperationException("Cannot accept registration: Event is at Max Registrations");
            }
            var registrationWithGivenId = _registrations.FirstOrDefault(x => x.Id == registrationId);
            if (registrationWithGivenId == null)
            {
                throw new EventRegistrationAggregateArgumentException("Cannot accept registration: no registration with the given id was found.", nameof(registrationId));
            }
            registrationWithGivenId.UpdateStatusAccepted();
        }
    }
}
