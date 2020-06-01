using EventsCore.Domain.Common;
using EventsCore.Domain.ValueObjects;
using EventsCore.Domain.Exceptions.Event;
using System;

namespace EventsCore.Domain.Entities
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
        /// <param name="dates">An <see cref="EventDates"></see> ValueObject containing the Event's Date information.</param>
        /// <param name="rules">A <see cref="EventRegistrationRules"></see> ValueObject containing the Registration rules for the event.</param>
        /// <param name="eventTypeId">An integer representing the <see cref="EventType"></see> Id of the event.</param>
        /// <param name="eventSeriesId">An optional integer representing the <see cref="EventSeries"></see> Id of the EventSeries to which the new event will belong.</param>
        public Event(string title, string description, EventDates dates, EventRegistrationRules rules, int eventTypeId, int eventSeriesId = 0)
        {
            UpdateTitle(title);
            UpdateDescription(description);
            UpdateEventDates(dates);
            UpdateRegistrationRules(rules);   
            UpdateEventType(eventTypeId);
            if (eventSeriesId != 0)
            {
                AddEventToSeries(eventSeriesId);
            }
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
        /// The Id of the <see cref="EventSeries"></see> to which the Event belongs.
        /// </summary>
        public int? EventSeriesId { get; private set; }
        /// <summary>
        /// The <see cref="EventSeries"></see> to which the event belongs.
        /// </summary>
        public EventSeries EventSeries { get; private set; }
        /// <summary>
        /// The Id of the <see cref="EventType"></see> to which the Event belongs.
        /// </summary>
        public int EventTypeId {get; private set; }
        /// <summary>
        /// The <see cref="EventType"></see> to which the Event belongs.
        /// </summary>
        public EventType EventType { get; private set; }
        /// <summary>
        /// The Dates associated with this Event.
        /// </summary>
        /// <remarks>
        /// This uses the <seealso cref="EventDates"/> ValueObject, which encapsulates and validates the event's Start Date/Time, End Date/Time,
        /// Registration Period Start Date/Time, and Registration Period End Date/Time.
        /// </remarks>
        public EventDates Dates { get; private set;}
        /// <summary>
        /// The Event's Start Date, obtained from the Event.Dates ruleset.
        /// </summary>
        public DateTime StartDate => Dates.StartDate;
        /// <summary>
        /// The Event's End Date, obtained from the Event.Dates ruleset.
        /// </summary>
        public DateTime EndDate => Dates.EndDate;
        /// <summary>
        /// The Event's Registration Period Start Date, obtained from the Event.Dates ruleset.
        /// </summary>
        public DateTime RegistrationStartDate => Dates.RegistrationStartDate;
        /// <summary>
        /// The Event's Registration Period End Date, obtained from the Event.Dates ruleset.
        /// </summary>
        public DateTime RegistrationEndDate => Dates.RegistrationEndDate;
        /// <summary>
        /// The Event's Registration Rules ruleset.
        /// </summary>
        public EventRegistrationRules Rules { get; private set;}        
        /// <summary>
        /// The Maximum number of registrations, obtained from the Event.Rules ruleset.
        /// </summary>
        public uint MaxRegistrations => Rules.MaxRegistrations;
     
        /// <summary>
        /// Updates the Event's Title Property.
        /// </summary>
        /// <param name="newTitle">A non-empty, non-whitespace string containing the new Title.</param>
        /// <exception cref="EventArgumentException">Thrown when the newTitle string parameter is null or whitespace.</exception>
        public void UpdateTitle(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
            {
                throw new EventArgumentException("Cannot update Event: parameter must not be null or empty string.", nameof(newTitle));
            }
            _title = newTitle;
        }
        /// <summary>
        /// Updates the Event's Description property.
        /// </summary>
        /// <param name="newDescription">A non-empty, non-whitespace string containing the new Description.</param>
        /// <exception cref="EventArgumentException">Thrown when the newDescription string parameter is null or whitespace.</exception>
        public void UpdateDescription(string newDescription)
        {
            if (string.IsNullOrWhiteSpace(newDescription))
            {
                throw new EventArgumentException("Cannot update Event: parameter must not be null or empty string.", nameof(newDescription));
            }
            _description = newDescription;
        }
        /// <summary>
        /// Adds the Event to the <see cref="EventSeries"></see> with the given Id.
        /// </summary>
        /// <param name="eventSeriesId">The Id of the <see cref="EventSeries"></see> to which the Event should be assigned.</param>
        public void AddEventToSeries(int eventSeriesId)
        {
            if (eventSeriesId == 0)
            {
                EventSeriesId = null;
            }
            else
            {
                EventSeriesId = eventSeriesId;
            }
        }
        /// <summary>
        /// Removes the Event from it's <see cref="EventSeries"></see>
        /// </summary>
        public void RemoveEventFromSeries()
        {
            EventSeriesId = null;
        }
        /// <summary>
        /// Updates the Event's <see cref="EventType"></see>
        /// </summary>
        /// <param name="newEventTypeId">The Id of the <see cref="EventType"></see> to assign to this Event.</param>
        /// <exception cref="EventArgumentException">Thrown when the EventTypeId is 0 or out of range.</exception>
        public void UpdateEventType(int newEventTypeId)
        {
            EventTypeId = newEventTypeId != 0 ? newEventTypeId : throw new EventArgumentException("Cannot update Event Type: parameter cannot be 0.", nameof(newEventTypeId));
        }
        /// <summary>
        /// Updates the Event's Dates property.
        /// </summary>
        /// <param name="newDates">A <see cref="EventDates"> object</see> containing the new Date set.</param>
        /// <exception cref="EventArgumentException">Thrown when the newDates parameter is null.</exception>
        public void UpdateEventDates(EventDates newDates)
        {
            Dates = newDates ?? throw new EventArgumentException("Cannot update Event: parameter must not be null.", nameof(newDates));
        }
        /// <summary>
        /// Updates the Event's <see cref="EventRegistrationRules"></see> ruleset.
        /// </summary>
        /// <param name="newRules">An <see cref="EventRegistrationRules"></see> object containing the new rules to assign to the Event.</param>
        /// <exception cref="EventArgumentException">Thrown when the newRules parameter is null.</exception>
        public void UpdateRegistrationRules(EventRegistrationRules newRules)
        {
            Rules = newRules ?? throw new EventArgumentException("Cannot update Event: parameter must not be null", nameof(newRules));
        }        
    }
}
