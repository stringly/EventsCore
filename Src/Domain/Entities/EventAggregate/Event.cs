using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.EventAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventsCore.Domain.Entities.EventAggregate
{
    /// <summary>
    /// Domain Entity that represents an Event for which Users can register.
    /// </summary>
    public class Event : IEntity, IAggregateRoot
    {
        private Event() { }
        /// <summary>
        /// Creates a new Event object
        /// </summary>
        /// <param name="title">A string containing the Event's Title. Must not be null or only whitespace.</param>
        /// <param name="description">A string containing the Event's Description. Must not be null or only whitespace.</param>
        /// <param name="dates">An </param>
        public Event(string title, string description, EventDates dates, EventRegistrationRules rules)
        {
            UpdateTitle(title);
            UpdateDescription(description);
            UpdateEventDates(dates);
            UpdateRegistrationRules(rules);
            
        }
        /// <summary>
        /// Event's Primary Key
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// A string that represents the Event's title. 
        /// </summary>
        /// <remarks>
        /// This is a private field that can only be set via the .UpdateTitle() method.
        /// </remarks>
        private string _title;
        /// <summary>
        /// Returns a string containing the Event's Title
        /// </summary>
        public string Title => _title;
        /// <summary>
        /// A string containing the Event's Description.
        /// </summary>
        /// <remarks>
        /// This is a private field that can only be assigned or changed via the .UpdateDescription() method.
        /// </remarks>
        private string _description;
        /// <summary>
        /// Returns a string containing the Event's Description.
        /// </summary>
        public string Description => _description;
        /// <summary>
        /// The Dates associated with this Event.
        /// </summary>
        /// <remarks>
        /// This uses the <seealso cref="EventDates"/>EventDates value object, which encapsulates and validates the event's Start Date/Time, End Date/Time,
        /// Registration Period Start Date/Time, and Registration Period End Date/Time.
        /// </remarks>
        public EventDates Dates { get; private set;}
        public DateTime StartDate => Dates.StartDate;
        public DateTime EndDate => Dates.EndDate;
        public DateTime RegistrationStartDate => Dates.RegistrationStartDate;
        public DateTime RegistrationEndDate => Dates.RegistrationEndDate;
        public EventRegistrationRules Rules { get; private set;}        
        public uint MaxRegistrations => Rules.MaxRegistrations;
        private readonly List<Registration> _registrations;
        public IReadOnlyCollection<Registration> Registrations => _registrations;

        public int CurrentAttendeesCount => _registrations.Count(x => x.Status == RegistrationStatus.Accepted);
        public int CurrentStandbyCount => _registrations.Count(x => x.Status == RegistrationStatus.Standby);
        public bool IsAcceptingRegistrations { get {
                if (StartDate < DateTime.Now)
                {
                    return false;
                }
                else if (RegistrationStartDate > DateTime.Now || RegistrationEndDate < DateTime.Now)
                {
                    return false;
                }
                else if(MaxRegistrations >= CurrentAttendeesCount)
                {
                   return false;
                }
                else
                {
                    return true;
                }
            } 
        }        

        public void UpdateTitle(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
            {
                throw new EventArgumentException("Cannot update Event: parameter must not be null or empty string.", nameof(newTitle));
            }
            _title = newTitle;
        }
        public void UpdateDescription(string newDescription)
        {
            if (string.IsNullOrWhiteSpace(newDescription))
            {
                throw new EventArgumentException("Cannot update Event: parameter must not be null or empty string.", nameof(newDescription));
            }
            _description = newDescription;
        }
        public void UpdateEventDates(EventDates newDates)
        {
            Dates = newDates ?? throw new EventArgumentException("Cannot update Event: parameter must not be null.", nameof(newDates));
        }
        public void UpdateRegistrationRules(EventRegistrationRules newRules)
        {
            if(newRules == null)
            {
                throw new EventArgumentException("Cannot update Event: parameter must not be null", nameof(newRules));
            }
            Rules = newRules;
        }
        public void RegisterUser(int userId, string userName, string email, string contact)
        {
            if (!IsAcceptingRegistrations)
            {
                throw new EventInvalidOperationException("Cannot add Registration: Event is not accepting Registrations");
            }
            if (userId == 0)
            {
                throw new EventArgumentException("Cannot add Registration: UserId cannot be 0.", nameof(userId));
            }
            else if (string.IsNullOrEmpty(userName))
            {
                throw new EventArgumentException("Cannot add Registration: UserName cannot be null/empty string.", nameof(userName));
            }
            else if (string.IsNullOrEmpty(email))
            {
                throw new EventArgumentException("Cannot add Registration: User email cannot be null/empty string.", nameof(email));
            }
            else if (_registrations.Exists(x => x.UserRegistered.UserId == userId))
            {
                throw new EventInvalidOperationException("Cannot add Registration: User is already registered for this event.");
            }
            var registrant = new Registrant(userId, userName, email, contact);
            
            _registrations.Add(new Registration(registrant));
        }
        public void UnregisterUserByUserId(int userId)
        {
            var registrationForUser = _registrations.FirstOrDefault(x => x.UserRegistered.UserId == userId);

            if(registrationForUser == null)
            {
                throw new EventArgumentException("Cannot remove registration: no registration for user id found.", nameof(userId));
            }
            _registrations.Remove(registrationForUser);
        }
        public void UnregisterUserByRegistrationId(int registrationId)
        {
            var registrationWithGivenId = _registrations.FirstOrDefault(x => x.Id == registrationId);
            if (registrationWithGivenId == null)
            {
                throw new EventArgumentException("Cannot remove registration: no registration with the given id was found.", nameof(registrationId));
            }
            _registrations.Remove(registrationWithGivenId);
        }
        public void AcceptRegistrationById(int registrationId)
        {
            if (StartDate < DateTime.Now)
            {
                throw new EventInvalidOperationException("Cannot accept registration: Event has expired.");
            }
            else if (CurrentAttendeesCount >= MaxRegistrations)
            {
                throw new EventInvalidOperationException("Cannot accept registration: Event is at Max Registrations");
            }
            var registrationWithGivenId = _registrations.FirstOrDefault(x => x.Id == registrationId);
            if (registrationWithGivenId == null)
            {
                throw new EventArgumentException("Cannot accept registration: no registration with the given id was found.", nameof(registrationId));
            }
            registrationWithGivenId.UpdateStatusAccepted();
        }        
    }
}
