using EventsCore.Domain.Common;
using EventsCore.Domain.Entities.ValueObjects;
using EventsCore.Domain.Exceptions.EventRegistrationsAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventsCore.Domain.Entities.EventRegistrationsAggregate
{
    /// <summary>
    /// EventRegistrations Aggregate
    /// </summary>
    /// <remarks>
    /// Aggregate that is used to control Registration operations for an <see cref="Event">Event.</see>
    /// </remarks>
    public class EventRegistrations : IAggregateRoot
    {
        /// <summary>
        /// Parameterless constructor for EF
        /// </summary>
        private EventRegistrations() { }
        /// <summary>
        /// Creates a new instance of EventRegistrations
        /// </summary>
        /// <param name="eventId">The Id of the <see cref="Event">associated with this instance</see></param>
        /// <param name="eventDates">A instance of <see cref="EventDates">EventDates</see> that contains the dates associated with this event.</param>
        /// <param name="registrationRules">An instance of <see cref="EventRegistrationRules">RegistrationRules</see> that contain the Registration Rules for this event.</param>
        /// <param name="dateTimeProvider">An instance of <see cref="IDateTime"></see></param>
        public EventRegistrations(int eventId, EventDates eventDates, EventRegistrationRules registrationRules, IDateTime dateTimeProvider)
        {
            _dateTime = dateTimeProvider;
            EventId = eventId != 0 ? eventId : throw new EventRegistrationAggregateArgumentException("Cannot create Event: eventId is invalid.", nameof(eventId));
            EventDates = eventDates ?? throw new EventRegistrationAggregateArgumentException("Cannot create Event: eventDates cannot be null.", nameof(eventDates));
            Rules = registrationRules ?? throw new EventRegistrationAggregateArgumentException("Cannot create Event: registrationRules cannot be null", nameof(registrationRules));
            _registrations = new List<Registration>();
        }
        /// <summary>
        /// Private IDateTime date provider
        /// </summary>
        private readonly IDateTime _dateTime;

        /// <summary>
        /// The ID of the Event associated with this instance
        /// </summary>
        public int EventId { get; private set; }
        /// <summary>
        /// An ValueObject instance of the Dates associated with this event
        /// </summary>
        /// <remarks>
        /// An instance of <see cref="EventDates">EventDates</see>
        /// </remarks>
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
        /// <summary>
        /// The Event's Registration Ruleset
        /// </summary>
        /// <remarks>
        /// This is an instance of <see cref="EventRegistrationRules"></see>
        /// </remarks>
        public EventRegistrationRules Rules { get; private set; }
        /// <summary>
        /// The Maximum number of registrations, obtained from the Event.Rules ruleset.
        /// </summary>
        public uint MaxRegistrations => Rules.MaxRegistrations;
        private readonly List<Registration> _registrations;
        /// <summary>
        /// Readonly collection of <see cref="Registration"> Registrations </see>for this event.
        /// </summary>
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
                if (IsExpired)
                {
                    return false;
                }
                else if (RegistrationStartDate > _dateTime.Now || RegistrationEndDate < _dateTime.Now)
                {
                    return false;
                }
                else if (CurrentAttendeesCount >= MaxRegistrations)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        /// <summary>
        /// Returns the event's expiration status
        /// </summary>
        /// <remarks>
        /// Returns true if the Event's End Date is in the past, otherwise false.
        /// </remarks>
        public bool IsExpired {
            get { return EndDate < _dateTime.Now; }
        }
        /// <summary>
        /// Returns the event's active status
        /// </summary>
        /// <remarks>
        /// Returns true if the event's StartDate is in the past, but the event's end date is in the future.
        /// </remarks>
        public bool IsActive {
            get { return StartDate <= _dateTime.Now && EndDate >= _dateTime.Now;}
        } 
        /// <summary>
        /// Returns whether registrations can be placed on "Stanby" for the event
        /// </summary>
        /// <remarks>
        /// Returns true if the Maximum Standby registrations count has not been reached, otherwise false.
        /// </remarks>
        public bool IsStandByAvailable {
            get { return Rules.MaxStandbyRegistrations > 0 && CurrentStandbyCount < Rules.MaxStandbyRegistrations;}
        }
        /// <summary>
        /// Creates a new <see cref="Registration">Registration</see> and adds it to the Event's Registrations collection.
        /// </summary>
        /// <param name="userId">The integer Id of the User.</param>
        /// <param name="userName">A string containing the Display Name of the User.</param>
        /// <param name="email">A string containing the User's email address.</param>
        /// <param name="contact">A string containing the User's primary contact phone number.</param>
        /// <exception cref="EventRegistrationAggregateInvalidOperationException">
        /// Throw when:
        /// <list type="bullet">
        ///     <item><description>Event is not accepting registrations</description></item>
        ///     <Item><description>When the UserId parameter is already registered for the Event</description></Item> 
        /// </list> 
        /// </exception>
        /// <exception cref="EventRegistrationAggregateArgumentException">
        /// Thrown when:
        /// <list type="bullet">
        ///     <item><description>The userId parameter is 0 or out of range</description></item>
        ///     <item><description>The userName parameter is empty/whitespace string</description></item>
        ///     <item><description>The email parameter is empty/whitespace string</description></item>
        /// </list>
        /// </exception>
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
        /// <summary>
        /// Removes a Registration with the provided UserId from the Event's registration collection 
        /// </summary>
        /// <param name="userId">The integer ID for the User to whom the registration belongs</param>
        /// <exception cref="EventRegistrationAggregateArgumentException">
        /// Thrown when no <see cref="Registration">Registration</see> for the given UserId was found in the Event's Registration collection.
        /// </exception>
        public void DeleteRegistrationByUserId(int userId)
        {
            var registrationForUser = _registrations.FirstOrDefault(x => x.UserId == userId);

            if (registrationForUser == null)
            {
                throw new EventRegistrationAggregateArgumentException("Cannot remove registration: no registration for user id found.", nameof(userId));
            }
            _registrations.Remove(registrationForUser);
        }
        /// <summary>
        /// Updates the <see cref="RegistrationStatus"></see> of a Registration to "Accepted" by the id of the UserId associated with the <see cref="Registration"></see>
        /// </summary>
        /// <param name="userId">The UserId of the <see cref="User"></see> associated with the <see cref="Registration"></see></param>
        /// <exception cref="EventRegistrationAggregateInvalidOperationException">
        /// Thrown when 
        /// <list type="bullet">
        /// <item><description>the <see cref="Event"></see> is expired.</description></item>
        /// <item><description>the <see cref="Event"></see> is at Maximum allowed "Accepted" registrations.</description></item>
        /// </list>
        /// </exception>
        /// <exception cref="EventRegistrationAggregateArgumentException">Thrown when no Registration with the given UserId parameter could be found in the <see cref="EventRegistrations._registrations"></see> collection.</exception>
        public void AcceptRegistrationByUserId(int userId)
        {
            if (IsExpired)
            {
                throw new EventRegistrationAggregateInvalidOperationException("Cannot accept registration: Event has expired.");
            }
            else if (CurrentAttendeesCount >= MaxRegistrations)
            {
                throw new EventRegistrationAggregateInvalidOperationException("Cannot accept registration: Event is at Max Registrations");
            }
            var registrationForUserId = _registrations.FirstOrDefault(x => x.UserId == userId);
            if (registrationForUserId == null)
            {
                throw new EventRegistrationAggregateArgumentException("Cannot accept registration: no registration with the given id was found.", nameof(userId));
            }
            registrationForUserId.UpdateStatusAccepted();
        }
        public void RejectRegistrationByUserId(int userId)
        {
            if (IsExpired)
            {
                throw new EventRegistrationAggregateInvalidOperationException("Cannot reject registration: Event has expired.");
            }
            else
            {
                var registrationForUserId = _registrations.FirstOrDefault(x => x.UserId == userId);
                if(registrationForUserId == null)
                {
                    throw new EventRegistrationAggregateArgumentException("Cannot reject registration: no registration with the given id was found.", nameof(userId));
                }
                else
                {
                    registrationForUserId.UpdateStatusRejected();
                }
            }
        }
        public void StandbyRegistrationByUserId(int userId)
        {
            if (IsExpired)
            {
                throw new EventRegistrationAggregateInvalidOperationException("Cannot standby registration: Event has expired.");
            }
            else if(!IsStandByAvailable)
            {
                throw new EventRegistrationAggregateInvalidOperationException("Cannot standby registration: Standby status not available for this Event.");
            }
            var registrationForUserId = _registrations.FirstOrDefault(x => x.UserId == userId);
            if (registrationForUserId == null)
            {
                throw new EventRegistrationAggregateArgumentException("Cannot standby registration: no registration with the given id was found.", nameof(userId));
            }
            registrationForUserId.UpdateStatusStandby();
        }        
    }
}
