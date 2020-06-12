using EventsCore.Domain.Common;
using EventsCore.Domain.ValueObjects;
using EventsCore.Domain.Exceptions.Event;
using System;
using EventsCore.Domain.Entities.EventAttendanceAggregate;
using EventsCore.Domain.Entities.EventRegistrationsAggregate;
using EventsCore.Domain.Entities.EventModulesAggregate;
using EventsCore.Domain.Exceptions.ValueObjects;

namespace EventsCore.Domain.Entities
{
    /// <summary>
    /// Domain Entity that represents an Event for which Users can register.
    /// </summary>
    public class Event : BaseEntity, IAggregateRoot
    {
        private Event() { }
        /// <summary>
        /// Creates a new Event object
        /// </summary>
        /// <param name="title">A string containing the Event's Title. Must not be null or only whitespace.</param>
        /// <param name="description">A string containing the Event's Description. Must not be null or only whitespace.</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="regStartDate"></param>
        /// <param name="regEndDate"></param>
        /// <param name="maxRegs"></param>
        /// <param name="minRegs"></param>
        /// <param name="maxStandby"></param>
        /// <param name="eventTypeId">An integer representing the <see cref="EventType"></see> Id of the event.</param>
        /// <param name="eventSeriesId">An optional integer representing the <see cref="EventSeries"></see> Id of the EventSeries to which the new event will belong.</param>
        public Event(
            string title, 
            string description, 
            DateTime startDate,
            DateTime endDate, 
            DateTime regStartDate, 
            DateTime regEndDate,
            int maxRegs,
            int? minRegs,
            int? maxStandby,
            int eventTypeId, 
            int? eventSeriesId)
        {
            UpdateTitle(title);
            UpdateDescription(description);
            UpdateEventDates(startDate, endDate, regStartDate, regEndDate);
            UpdateRegistrationRules(maxRegs, minRegs, maxStandby);   
            UpdateEventType(eventTypeId);
            if (eventSeriesId != null)
            {
                AddEventToSeries((int)eventSeriesId);
            }
        }
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
        /// The <see cref="EventAttendanceAggregate.EventAttendance"></see> aggregate built from this event
        /// </summary>
        public EventAttendance EventAttendance { get; private set;}
        /// <summary>
        /// The <see cref="EventRegistrations"></see> aggregate built from this event
        /// </summary>
        public EventRegistrations Registrations { get; private set;}
        /// <summary>
        /// THe <see cref="EventModules"></see> aggregate built from this event.
        /// </summary>
        public EventModules Modules { get; private set;}
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
        /// Updates the Event's <see cref="Dates"></see>
        /// </summary>
        /// <param name="startDate">A nullable DateTime containing the Event's start date.</param>
        /// <param name="endDate"></param>
        /// <param name="regStart"></param>
        /// <param name="regEnd"></param>
        public void UpdateEventDates(DateTime? startDate, DateTime? endDate, DateTime? regStart, DateTime? regEnd)
        {
            if (startDate == null && endDate == null && regStart == null && regEnd == null)
            {
                throw new EventArgumentException("Cannot update Event: at least one Event Date parameter is required.", nameof(Event.Dates));
            }
            try
            {
                EventDates newDates = new EventDates(
                    startDate != null ? (DateTime)startDate : Dates.StartDate,
                    endDate != null ? (DateTime)endDate : Dates.EndDate,
                    regStart != null ? (DateTime)regStart : Dates.RegistrationStartDate,
                    regEnd != null ? (DateTime)regStart : Dates.RegistrationEndDate            
                    );
                Dates = newDates;
            }
            catch(EventDatesInvalidException ex)
            {
                throw new EventArgumentException(ex.Message, nameof(Event.Dates));
            }            
        }
        /// <summary>
        /// Updates the Event's <see cref="Rules"></see>
        /// </summary>
        /// <remarks>
        /// This method's parameters are defaulted to null, but if no parameters are provided, an error will be thrown.
        /// If at least one parameter is provided, the method will attempt to update the Rules to reflect the updated values.
        /// </remarks>
        /// <param name="maxRegs">Nullable int containing the max registration count.</param>
        /// <param name="minRegs">Nullable int containing the min registration count.</param>
        /// <param name="maxStandbyRegs">Nullable int containing the max standby registration count.</param>
        /// <exception cref="EventArgumentException">
        /// Thrown when:
        /// <list type="bullet">
        /// <item><description>No parameters are provided.</description></item>
        /// <item><description>One of the provided parameters violated a constraint in the <see cref="EventRegistrationRules"></see> constructor. Most commonly, this will be when the MaxRegistration count is less than the MinRegistraitonCount </description></item>
        /// </list>
        /// </exception>
        public void UpdateRegistrationRules(int? maxRegs, int? minRegs, int? maxStandbyRegs)
        {
            if(maxRegs == null && minRegs == null && maxStandbyRegs == null)
            {
                throw new EventArgumentException("Cannot update Event: at least one registration rules parameter required.", nameof(Event.Rules));
            }            
            try
            {
                EventRegistrationRules newRules = new EventRegistrationRules(
                    maxRegs != null ? (uint)maxRegs : Rules.MaxRegistrations,
                    minRegs != null ? (uint)minRegs : Rules.MinRegistrations,
                    maxStandbyRegs != null ? (uint)maxStandbyRegs : Rules.MaxStandbyRegistrations);
            
                Rules = newRules;
            }
            catch (EventRegistrationRulesArgumentException e)
            {
                throw new EventArgumentException(e.Message, nameof(Event.Rules));
            }
        }        
    }
}
