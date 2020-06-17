using EventsCore.Domain.Common;
using EventsCore.Domain.ValueObjects;
using EventsCore.Domain.Exceptions.Event;
using System;
using EventsCore.Domain.Exceptions.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace EventsCore.Domain.Entities
{
    /// <summary>
    /// Domain Entity that represents an Event for which Users can register.
    /// </summary>
    public class Event : BaseEntity, IAggregateRoot
    {
        private Event() { }
        /// <summary>
        /// Minimum constructor to create a valid instance of an Event
        /// </summary>
        /// <param name="title">A string containing the Event's Title. Must not be null or only whitespace.</param>
        /// <param name="description">A string containing the Event's Description. Must not be null or only whitespace.</param>
        /// <param name="eventTypeId">An integer representing the <see cref="EventType"></see> Id of the event.</param>
        /// <param name="startDate">A DateTime containing the Event's Start Date. Must be before endDate</param>
        /// <param name="endDate">A DateTime containing the Event's End Date. Must be after startDate</param>
        /// <param name="regStartDate">A DateTime containing the Event's Registration period Start Date. Must be before Event Start Date and Registration Period End Date</param>
        /// <param name="regEndDate">A DateTime containing the Event's Registration period End Date. Must be after Registration Period Start Date and before Event Start Date.</param>
        /// <param name="maxRegs">An integer with the maximum number of attendees for the event.</param>
        public Event(
            string title, 
            string description,
            int eventTypeId,
            DateTime startDate,
            DateTime endDate,
            DateTime regStartDate,
            DateTime regEndDate,
            int maxRegs
            ) : this(title, description, eventTypeId, startDate, endDate, regStartDate, regEndDate)
        {
            UpdateRegistrationRules(maxRegs, null, null);
        }
        /// <summary>
        /// Constructor to create a valid instance of an Event with a physical address
        /// </summary>
        /// <param name="title">A string containing the Event's Title. Must not be null or only whitespace.</param>
        /// <param name="description">A string containing the Event's Description. Must not be null or only whitespace.</param>
        /// <param name="eventTypeId">An integer representing the <see cref="EventType"></see> Id of the event.</param>
        /// <param name="startDate">A DateTime containing the Event's Start Date. Must be before endDate</param>
        /// <param name="endDate">A DateTime containing the Event's End Date. Must be after startDate</param>
        /// <param name="regStartDate">A DateTime containing the Event's Registration period Start Date. Must be before Event Start Date and Registration Period End Date</param>
        /// <param name="regEndDate">A DateTime containing the Event's Registration period End Date. Must be after Registration Period Start Date and before Event Start Date.</param>
        /// <param name="maxRegs">An integer with the maximum number of attendees for the event.</param>
        /// <param name="street">The street address, e.g. "123 Anywhere St." Required, cannot be null/whitespace/empty string.</param>
        /// <param name="suite">The suite/apartment/room number. This is an optional field.</param>
        /// <param name="city">The name of the city in which the address is located. Required, cannot be null/whitespace/empty string.</param>
        /// <param name="state">The 2-digit Postal Abbreviation for the state in which the address is located. Required, cannot be null/whitespace/empty string.</param>        
        /// <param name="zip">The 5-digit ZIP code for the address. Required, cannot be null/whitespace/empty string.</param>
        public Event(string title,
            string description,
            int eventTypeId,
            DateTime startDate,
            DateTime endDate,
            DateTime regStartDate,
            DateTime regEndDate,
            int maxRegs, 
            string street, 
            string suite,
            string city, 
            string state, 
            string zip
            ) : this(title, description, eventTypeId, startDate, endDate, regStartDate, regEndDate)
        {
            UpdateRegistrationRules(maxRegs, null, null);
            UpdateAddress(street, suite, city, state, zip);
        }
        /// <summary>
        /// Overload of Constructor that allows setting of Max and min registration counts.
        /// </summary>
        /// <param name="title">A string containing the Event's Title. Must not be null or only whitespace.</param>
        /// <param name="description">A string containing the Event's Description. Must not be null or only whitespace.</param>
        /// <param name="eventTypeId">An integer representing the <see cref="EventType"></see> Id of the event.</param>
        /// <param name="startDate">A DateTime containing the Event's Start Date. Must be before endDate</param>
        /// <param name="endDate">A DateTime containing the Event's End Date. Must be after startDate</param>
        /// <param name="regStartDate">A DateTime containing the Event's Registration period Start Date. Must be before Event Start Date and Registration Period End Date</param>
        /// <param name="regEndDate">A DateTime containing the Event's Registration period End Date. Must be after Registration Period Start Date and before Event Start Date.</param>
        /// <param name="maxRegs">An integer with the maximum number of attendees for the event.</param>
        /// <param name="minRegs">An integer with the minimum number of attendees for the event. Must be less than or equal to maxRegs.</param>
        public Event(
            string title,
            string description,
            int eventTypeId,
            DateTime startDate,
            DateTime endDate,
            DateTime regStartDate,
            DateTime regEndDate,
            int maxRegs, 
            int minRegs
            ) : this(title, description, eventTypeId, startDate, endDate, regStartDate, regEndDate)
        {
            UpdateRegistrationRules(maxRegs, minRegs, null);
        }
        /// <summary>
        /// Overload of Constructor that allows setting of Max and min registration counts and physical location.
        /// </summary>
        /// <param name="title">A string containing the Event's Title. Must not be null or only whitespace.</param>
        /// <param name="description">A string containing the Event's Description. Must not be null or only whitespace.</param>
        /// <param name="eventTypeId">An integer representing the <see cref="EventType"></see> Id of the event.</param>
        /// <param name="startDate">A DateTime containing the Event's Start Date. Must be before endDate</param>
        /// <param name="endDate">A DateTime containing the Event's End Date. Must be after startDate</param>
        /// <param name="regStartDate">A DateTime containing the Event's Registration period Start Date. Must be before Event Start Date and Registration Period End Date</param>
        /// <param name="regEndDate">A DateTime containing the Event's Registration period End Date. Must be after Registration Period Start Date and before Event Start Date.</param>
        /// <param name="maxRegs">An integer with the maximum number of attendees for the event.</param>
        /// <param name="minRegs">An integer with the minimum number of attendees for the event. Must be less than or equal to maxRegs.</param>
        /// <param name="street">The street address, e.g. "123 Anywhere St." Required, cannot be null/whitespace/empty string.</param>
        /// <param name="suite">The suite/apartment/room number. This is an optional field.</param>
        /// <param name="city">The name of the city in which the address is located. Required, cannot be null/whitespace/empty string.</param>
        /// <param name="state">The 2-digit Postal Abbreviation for the state in which the address is located. Required, cannot be null/whitespace/empty string.</param>        
        /// <param name="zip">The 5-digit ZIP code for the address. Required, cannot be null/whitespace/empty string.</param>
        public Event(
            string title, 
            string description,
            int eventTypeId, 
            DateTime startDate,
            DateTime endDate,
            DateTime regStartDate,
            DateTime regEndDate,
            int maxRegs,
            int minRegs,
            string street,
            string suite,
            string city, 
            string state,
            string zip) : this(title, description, eventTypeId, startDate, endDate, regStartDate, regEndDate)
        {
            UpdateRegistrationRules(maxRegs, minRegs, null);
            UpdateAddress(street, suite, city, state, zip);
        }
        /// <summary>
        /// Overload of Constructor that allows setting of Max/Min registrations and max standby registrations.
        /// </summary>
        /// <param name="title">A string containing the Event's Title. Must not be null or only whitespace.</param>
        /// <param name="description">A string containing the Event's Description. Must not be null or only whitespace.</param>
        /// <param name="eventTypeId">An integer representing the <see cref="EventType"></see> Id of the event.</param>
        /// <param name="startDate">A DateTime containing the Event's Start Date. Must be before endDate</param>
        /// <param name="endDate">A DateTime containing the Event's End Date. Must be after startDate</param>
        /// <param name="regStartDate">A DateTime containing the Event's Registration period Start Date. Must be before Event Start Date and Registration Period End Date</param>
        /// <param name="regEndDate">A DateTime containing the Event's Registration period End Date. Must be after Registration Period Start Date and before Event Start Date.</param>
        /// <param name="maxRegs">An integer with the maximum number of attendees for the event.</param>
        /// <param name="minRegs">An integer with the minimum number of attendees for the event. Must be less than or equal to maxRegs.</param>
        /// <param name="maxStandbyRegs">An integer with the maximum number of standby registrations for the event. Must be less than or equal to maxRegs.</param>
        public Event(
            string title,
            string description,
            int eventTypeId,
            DateTime startDate,
            DateTime endDate,
            DateTime regStartDate,
            DateTime regEndDate,
            int maxRegs,
            int minRegs,
            int maxStandbyRegs
            ) : this(title, description, eventTypeId, startDate, endDate, regStartDate, regEndDate)
        {

            UpdateRegistrationRules(maxRegs, minRegs, maxStandbyRegs);
        }
        /// <summary>
        /// Overload of Constructor that allows setting of Max/Min registrations and max standby registrations and physical address.
        /// </summary>
        /// <param name="title">A string containing the Event's Title. Must not be null or only whitespace.</param>
        /// <param name="description">A string containing the Event's Description. Must not be null or only whitespace.</param>
        /// <param name="eventTypeId">An integer representing the <see cref="EventType"></see> Id of the event.</param>
        /// <param name="startDate">A DateTime containing the Event's Start Date. Must be before endDate</param>
        /// <param name="endDate">A DateTime containing the Event's End Date. Must be after startDate</param>
        /// <param name="regStartDate">A DateTime containing the Event's Registration period Start Date. Must be before Event Start Date and Registration Period End Date</param>
        /// <param name="regEndDate">A DateTime containing the Event's Registration period End Date. Must be after Registration Period Start Date and before Event Start Date.</param>
        /// <param name="maxRegs">An integer with the maximum number of attendees for the event.</param>
        /// <param name="minRegs">An integer with the minimum number of attendees for the event. Must be less than or equal to maxRegs.</param>
        /// <param name="maxStandbyRegs">An integer with the maximum number of standby registrations for the event. Must be less than or equal to maxRegs.</param>
        /// <param name="street">The street address, e.g. "123 Anywhere St." Required, cannot be null/whitespace/empty string.</param>
        /// <param name="suite">The suite/apartment/room number. This is an optional field.</param>
        /// <param name="city">The name of the city in which the address is located. Required, cannot be null/whitespace/empty string.</param>
        /// <param name="state">The 2-digit Postal Abbreviation for the state in which the address is located. Required, cannot be null/whitespace/empty string.</param>        
        /// <param name="zip">The 5-digit ZIP code for the address. Required, cannot be null/whitespace/empty string.</param>
        public Event(
            string title,
            string description,
            int eventTypeId,
            DateTime startDate,
            DateTime endDate,
            DateTime regStartDate,
            DateTime regEndDate,
            int maxRegs,
            int minRegs,
            int maxStandbyRegs, 
            string street,
            string suite,
            string city, 
            string state,
            string zip
            ) : this(title, description, eventTypeId, startDate, endDate, regStartDate, regEndDate)
        {

            UpdateRegistrationRules(maxRegs, minRegs, maxStandbyRegs);
            UpdateAddress(street, suite, city, state, zip);
        }
        /// <summary>
        /// Overload of constructor that allows the EventSeriesId and Max Registrations to be set.
        /// </summary>
        /// <param name="title">A string containing the Event's Title. Must not be null or only whitespace.</param>
        /// <param name="description">A string containing the Event's Description. Must not be null or only whitespace.</param>
        /// <param name="eventTypeId">An integer representing the <see cref="EventType"></see> Id of the event.</param>
        /// <param name="eventSeriesId">An integer representing the Id of the <see cref="EventSeries"></see> to which this event will be assigned.</param>
        /// <param name="startDate">A DateTime containing the Event's Start Date. Must be before endDate</param>
        /// <param name="endDate">A DateTime containing the Event's End Date. Must be after startDate</param>
        /// <param name="regStartDate">A DateTime containing the Event's Registration period Start Date. Must be before Event Start Date and Registration Period End Date</param>
        /// <param name="regEndDate">A DateTime containing the Event's Registration period End Date. Must be after Registration Period Start Date and before Event Start Date.</param>
        /// <param name="maxRegs">An integer with the maximum number of attendees for the event.</param>
        public Event(
            string title,
            string description,
            int eventTypeId,
            int eventSeriesId,
            DateTime startDate,
            DateTime endDate,
            DateTime regStartDate,
            DateTime regEndDate,
            int maxRegs
            ) : this(title, description, eventTypeId, startDate, endDate, regStartDate, regEndDate)
        {
            UpdateRegistrationRules(maxRegs, null, null);
            AddEventToSeries(eventSeriesId);
        }
        /// <summary>
        /// Overload of constructor that allows the EventSeriesId and Max Registrations to be set.
        /// </summary>
        /// <param name="title">A string containing the Event's Title. Must not be null or only whitespace.</param>
        /// <param name="description">A string containing the Event's Description. Must not be null or only whitespace.</param>
        /// <param name="eventTypeId">An integer representing the <see cref="EventType"></see> Id of the event.</param>
        /// <param name="eventSeriesId">An integer representing the Id of the <see cref="EventSeries"></see> to which this event will be assigned.</param>
        /// <param name="startDate">A DateTime containing the Event's Start Date. Must be before endDate</param>
        /// <param name="endDate">A DateTime containing the Event's End Date. Must be after startDate</param>
        /// <param name="regStartDate">A DateTime containing the Event's Registration period Start Date. Must be before Event Start Date and Registration Period End Date</param>
        /// <param name="regEndDate">A DateTime containing the Event's Registration period End Date. Must be after Registration Period Start Date and before Event Start Date.</param>
        /// <param name="maxRegs">An integer with the maximum number of attendees for the event.</param>
        /// <param name="street">The street address, e.g. "123 Anywhere St." Required, cannot be null/whitespace/empty string.</param>
        /// <param name="suite">The suite/apartment/room number. This is an optional field.</param>
        /// <param name="city">The name of the city in which the address is located. Required, cannot be null/whitespace/empty string.</param>
        /// <param name="state">The 2-digit Postal Abbreviation for the state in which the address is located. Required, cannot be null/whitespace/empty string.</param>        
        /// <param name="zip">The 5-digit ZIP code for the address. Required, cannot be null/whitespace/empty string.</param>
        public Event(
            string title,
            string description,
            int eventTypeId,
            int eventSeriesId,
            DateTime startDate,
            DateTime endDate,
            DateTime regStartDate,
            DateTime regEndDate,
            int maxRegs,
            string street,
            string suite, 
            string city,
            string state,
            string zip
            ) : this(title, description, eventTypeId, startDate, endDate, regStartDate, regEndDate)
        {
            UpdateRegistrationRules(maxRegs, null, null);
            AddEventToSeries(eventSeriesId);
            UpdateAddress(street, suite, city, state, zip);
        }
        /// <summary>
        /// Overload of constructor that allows the EventSeriesId, Max Registrations, and Min Registrations to be set.
        /// </summary>
        /// <param name="title">A string containing the Event's Title. Must not be null or only whitespace.</param>
        /// <param name="description">A string containing the Event's Description. Must not be null or only whitespace.</param>
        /// <param name="eventTypeId">An integer representing the <see cref="EventType"></see> Id of the event.</param>
        /// <param name="eventSeriesId">An integer representing the Id of the <see cref="EventSeries"></see> to which this event will be assigned.</param>
        /// <param name="startDate">A DateTime containing the Event's Start Date. Must be before endDate</param>
        /// <param name="endDate">A DateTime containing the Event's End Date. Must be after startDate</param>
        /// <param name="regStartDate">A DateTime containing the Event's Registration period Start Date. Must be before Event Start Date and Registration Period End Date</param>
        /// <param name="regEndDate">A DateTime containing the Event's Registration period End Date. Must be after Registration Period Start Date and before Event Start Date.</param>
        /// <param name="maxRegs">An integer with the maximum number of attendees for the event.</param>
        /// <param name="minRegs">An integer with the minimum number of attendees for the event. Must be less than or equal to maxRegs.</param>
        public Event(
            string title,
            string description,
            int eventTypeId,
            int eventSeriesId,
            DateTime startDate,
            DateTime endDate,
            DateTime regStartDate,
            DateTime regEndDate,
            int maxRegs,
            int minRegs
            ) : this(title, description, eventTypeId, startDate, endDate, regStartDate, regEndDate)
        {
            UpdateRegistrationRules(maxRegs, minRegs, null);
            AddEventToSeries(eventSeriesId);
        }
        /// <summary>
        /// Overload of constructor that allows the physical address, EventSeriesId, Max Registrations, Min Registrations, and Max Standby Registrations to be set via nullable types.
        /// </summary>
        /// <param name="title">A string containing the Event's Title. Must not be null or only whitespace.</param>
        /// <param name="description">A string containing the Event's Description. Must not be null or only whitespace.</param>
        /// <param name="eventTypeId">An integer representing the <see cref="EventType"></see> Id of the event.</param>
        /// <param name="eventSeriesId">An integer representing the Id of the <see cref="EventSeries"></see> to which this event will be assigned.</param>
        /// <param name="startDate">A DateTime containing the Event's Start Date. Must be before endDate</param>
        /// <param name="endDate">A DateTime containing the Event's End Date. Must be after startDate</param>
        /// <param name="regStartDate">A DateTime containing the Event's Registration period Start Date. Must be before Event Start Date and Registration Period End Date</param>
        /// <param name="regEndDate">A DateTime containing the Event's Registration period End Date. Must be after Registration Period Start Date and before Event Start Date.</param>
        /// <param name="maxRegs">An integer with the maximum number of attendees for the event.</param>
        /// <param name="minRegs">An integer with the minimum number of attendees for the event. Must be less than or equal to maxRegs.</param>
        /// <param name="maxStandbyRegs">An integer with the maximum number of standby registrations for the event. Must be less than or equal to maxRegs.</param>
        /// <param name="street">The street address, e.g. "123 Anywhere St."</param>
        /// <param name="suite">The suite/apartment/room number. This is an optional field.</param>
        /// <param name="city">The name of the city in which the address is located.</param>
        /// <param name="state">The 2-digit Postal Abbreviation for the state in which the address is located.</param>        
        /// <param name="zip">The 5-digit ZIP code for the address.</param>
        public Event(
            string title,
            string description,
            int eventTypeId,
            int? eventSeriesId,
            DateTime startDate,
            DateTime endDate,
            DateTime regStartDate,
            DateTime regEndDate,
            int maxRegs,
            int? minRegs,
            int? maxStandbyRegs,
            string street,
            string suite,
            string city,
            string state,
            string zip
            ) : this(title, description, eventTypeId, startDate, endDate, regStartDate, regEndDate)
        {
            UpdateRegistrationRules(maxRegs, minRegs, maxStandbyRegs);
            if(eventSeriesId != null)
            {
                AddEventToSeries((int)eventSeriesId);
            }
            UpdateAddress(street, suite, city, state, zip);

            
        }
        /// <summary>
        /// Overload of constructor that allows the EventSeriesId, Max Registrations, Min Registrations, and Max Standby Registrations to be set.
        /// </summary>
        /// <param name="title">A string containing the Event's Title. Must not be null or only whitespace.</param>
        /// <param name="description">A string containing the Event's Description. Must not be null or only whitespace.</param>
        /// <param name="eventTypeId">An integer representing the <see cref="EventType"></see> Id of the event.</param>
        /// <param name="eventSeriesId">An integer representing the Id of the <see cref="EventSeries"></see> to which this event will be assigned.</param>
        /// <param name="startDate">A DateTime containing the Event's Start Date. Must be before endDate</param>
        /// <param name="endDate">A DateTime containing the Event's End Date. Must be after startDate</param>
        /// <param name="regStartDate">A DateTime containing the Event's Registration period Start Date. Must be before Event Start Date and Registration Period End Date</param>
        /// <param name="regEndDate">A DateTime containing the Event's Registration period End Date. Must be after Registration Period Start Date and before Event Start Date.</param>
        /// <param name="maxRegs">An integer with the maximum number of attendees for the event.</param>
        /// <param name="minRegs">An integer with the minimum number of attendees for the event. Must be less than or equal to maxRegs.</param>
        /// <param name="maxStandbyRegs">An integer with the maximum number of standby registrations for the event. Must be less than or equal to maxRegs.</param>
        public Event(
            string title,
            string description,
            int eventTypeId,
            int eventSeriesId,
            DateTime startDate,
            DateTime endDate,
            DateTime regStartDate,
            DateTime regEndDate,
            int maxRegs,
            int minRegs,
            int maxStandbyRegs
            ) : this(title, description, eventTypeId, startDate, endDate, regStartDate, regEndDate)
        {
            UpdateRegistrationRules(maxRegs, minRegs, maxStandbyRegs);
            AddEventToSeries(eventSeriesId);
        }
        /// <summary>
        /// Private, common parameter constructor used to chain constructors.
        /// </summary>
        /// <param name="title">A string containing the Event's Title. Must not be null or only whitespace.</param>
        /// <param name="description">A string containing the Event's Description. Must not be null or only whitespace.</param>
        /// <param name="eventTypeId">An integer representing the <see cref="EventType"></see> Id of the event.</param>
        /// <param name="startDate">A DateTime containing the Event's Start Date. Must be before endDate</param>
        /// <param name="endDate">A DateTime containing the Event's End Date. Must be after startDate</param>
        /// <param name="regStartDate">A DateTime containing the Event's Registration period Start Date. Must be before Event Start Date and Registration Period End Date</param>
        /// <param name="regEndDate">A DateTime containing the Event's Registration period End Date. Must be after Registration Period Start Date and before Event Start Date.</param>
        private Event(
            string title,
            string description,
            int eventTypeId,
            DateTime startDate,
            DateTime endDate,
            DateTime regStartDate,
            DateTime regEndDate)
        {
            UpdateTitle(title);
            UpdateDescription(description);
            UpdateEventDates(startDate, endDate, regStartDate, regEndDate);            
            UpdateEventType(eventTypeId);
            _modules = new List<Module>();
            _attendance = new List<Attendance>();
            _registrations = new List<Registration>();
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
        /// A <see cref="ValueObjects.Address"/> containing the Event's Address
        /// </summary>
        /// <remarks>
        /// This uses the <seealso cref="ValueObjects.Address"/> value object to store an event's address
        /// </remarks>
        public Address Address { get; private set; }
        /// <summary>
        /// Returns a string with the Event's Address, or "None" if no address exists.
        /// </summary>
        public string Location {
            get {
                if (Address != null)
                {
                    return $"{Address.Street}{(!String.IsNullOrEmpty(Address.Suite) ? $" {Address.Suite}" : " ")} {Address.City}, {Address.State} {Address.ZipCode}";
                }
                else
                {
                    return "None";
                }
            }
        }
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
        private List<Attendance> _attendance;
        /// <summary>
        /// A list of <see cref="Attendance"></see> records associated with this event.
        /// </summary>
        public IEnumerable<Attendance> Attendance => _attendance.AsReadOnly();
        private List<Registration> _registrations;
        /// <summary>
        /// A list of <see cref="Registration"></see> records associated with this event.
        /// </summary>
        public IEnumerable<Registration> Registrations => _registrations.AsReadOnly();
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
        public bool IsAcceptingRegistrations(IDateTime _dateTime) {
            
            if (IsExpired(_dateTime))
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
        /// <summary>
        /// Returns the event's expiration status
        /// </summary>
        /// <remarks>
        /// Returns true if the Event's End Date is in the past, otherwise false.
        /// </remarks>
        public bool IsExpired (IDateTime _dateTime)
        {
            return EndDate < _dateTime.Now; 
        }
        /// <summary>
        /// Returns the event's active status
        /// </summary>
        /// <remarks>
        /// Returns true if the event's StartDate is in the past, but the event's end date is in the future.
        /// </remarks>
        public bool IsActive (IDateTime _dateTime)
        {
            return StartDate <= _dateTime.Now && EndDate >= _dateTime.Now;
        }
        /// <summary>
        /// Returns whether registrations can be placed on "Stanby" for the event
        /// </summary>
        /// <remarks>
        /// Returns true if the Maximum Standby registrations count has not been reached, otherwise false.
        /// </remarks>
        public bool IsStandByAvailable {
            get { return Rules.MaxStandbyRegistrations > 0 && CurrentStandbyCount < Rules.MaxStandbyRegistrations; }
        }
        private List<Module> _modules;
        /// <summary>
        /// A list of <see cref="Module"></see> associated with this event.
        /// </summary>
        public IEnumerable<Module> Modules => _modules.AsReadOnly();
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
        /// Updates the event's <see cref="Address"/>
        /// </summary>
        /// <param name="street">A string containing the street address, i.e "123 Anywhere St."</param>
        /// <param name="suite">A string containing an optional suite, apt, room number, or other detail, i.e. "Suite #4"</param>
        /// <param name="city">A string containing the name of the city in which the address is located.</param>
        /// <param name="state">A string containing the 2-digit state postal code for the address.</param>
        /// <param name="zip">A strubg containing the 5-digit ZIP code for the address.</param>
        /// <exception cref="EventArgumentException">Thrown when one of the parameters is invalid and the <see cref="ValueObjects.Address"/> constructor throws an error.</exception>
        public void UpdateAddress(string street = "", string suite = "", string city = "", string state = "", string zip = "")
        {
            if (String.IsNullOrEmpty(street) && String.IsNullOrEmpty(suite) && String.IsNullOrEmpty(city) && String.IsNullOrEmpty(state) && string.IsNullOrEmpty(zip))
            {
                return;
            }
            try
            {
                if (this.Address != null)
                {
                    Address = new Address(
                        !String.IsNullOrWhiteSpace(street) ? street : this.Address.Street,
                        !String.IsNullOrWhiteSpace(suite) ? suite : this.Address.Suite,
                        !String.IsNullOrWhiteSpace(city) ? city : this.Address.City,
                        !String.IsNullOrWhiteSpace(state) ? state : this.Address.State,
                        !String.IsNullOrWhiteSpace(zip) ? zip : this.Address.ZipCode);
                }
                else
                {
                    Address = new Address(street, suite, city, state, zip);
                }
            }
            catch (AddressInvalidException ex)
            {
                throw new EventArgumentException($"Cannot update Event: {ex.Message}", nameof(Event.Address));
            }
            
        }
        /// <summary>
        /// Updates the Event's <see cref="Dates"></see>
        /// </summary>
        /// <param name="startDate">A nullable DateTime containing the Event's start date.</param>
        /// <param name="endDate"></param>
        /// <param name="regStart"></param>
        /// <param name="regEnd"></param>
        public void UpdateEventDates(DateTime? startDate = null, DateTime? endDate = null, DateTime? regStart = null, DateTime? regEnd = null)
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
                    regEnd != null ? (DateTime)regEnd : Dates.RegistrationEndDate            
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
        public void UpdateRegistrationRules(int? maxRegs = null, int? minRegs = null, int? maxStandbyRegs = null)
        {
            if(maxRegs == null && minRegs == null && maxStandbyRegs == null)
            {
                throw new EventArgumentException("Cannot update Event: at least one registration rules parameter required.", nameof(Event.Rules));
            }            
            try
            {
                EventRegistrationRules newRules = new EventRegistrationRules(
                    maxRegs != null ? (uint)maxRegs : Rules.MaxRegistrations,
                    minRegs != null ? (uint)minRegs : Rules?.MinRegistrations ?? 1,
                    maxStandbyRegs != null ? (uint)maxStandbyRegs : Rules?.MaxStandbyRegistrations ?? 0);
            
                Rules = newRules;
            }
            catch (EventRegistrationRulesArgumentException e)
            {
                throw new EventArgumentException(e.Message, nameof(Event.Rules));
            }
        }
        /// <summary>
        /// Creates a new <see cref="Module"></see> and adds it to the aggregate's <see cref="Modules"></see> collection.
        /// </summary>
        /// <param name="moduleName">A string containing the name of the module to be added.</param>
        /// <param name="moduleDescription">A string containing the description of the module to be added.</param>
        /// <exception cref="EventArgumentException">Thrown when a Module already exists with the given moduleName parameter.</exception>
        public void AddModule(string moduleName, string moduleDescription)
        {
            if (_modules.Exists(x => x.ModuleName == moduleName))
            {
                throw new EventArgumentException($"Cannot add Module to Event: module with Name {moduleName} already exists.", nameof(moduleName));
            }
            var newModule = new Module(moduleName, moduleDescription);
            _modules.Add(newModule);
        }
        /// <summary>
        /// Removes a module by Id
        /// </summary>
        /// <param name="moduleId">The Id of the module to be removed.</param>
        /// <exception cref="EventArgumentException">Thrown when no module with the given moduleId parameter exists in the aggregate's <see cref="Modules"></see> collection.</exception>
        public void RemoveModuleById(int moduleId)
        {
            var moduleWithGivenId = _modules.FirstOrDefault(x => x.Id == moduleId);
            if (moduleWithGivenId == null)
            {
                throw new EventArgumentException($"Cannot remove Module from Event: no module with id {moduleId} exists.", nameof(moduleId));
            }
            _modules.Remove(moduleWithGivenId);
        }
        /// <summary>
        /// Creates or updates an existing user attendance record to <see cref="AttendanceStatus.Present"></see>
        /// </summary>
        /// <param name="userId">The Id of the User to mark present</param>
        /// <param name="moduleId">The Id of the Module. This is required if the Event has modules</param>
        /// <exception cref="EventArgumentException">
        /// Thrown when the userId parameter is 0 or out of range.
        /// </exception>
        /// <exception cref="EventInvalidOperationException">
        /// Thrown when the Event has modules, but no moduleId parameter is provided.
        /// </exception>
        public void MarkUserPresent(int userId, int? moduleId = null)
        {
            if (userId == 0)
            {
                throw new EventArgumentException("Cannot mark User present: parameter must not be 0.", nameof(userId));
            }
            if (_modules.Count > 0 && moduleId == null)
            {
                throw new EventInvalidOperationException("Cannot mark User present: You must provide a module Id to update an attendance record for an Event that has modules.");
            }
            Attendance existingAttend = _attendance.FirstOrDefault(x => x.UserId == userId);
            if (existingAttend != null)
            {
                existingAttend.UpdateStatusPresent();
            }
            else
            {
                Attendance newAttend = new Attendance(userId, moduleId);
                _attendance.Add(newAttend);
            }

        }
        /// <summary>
        /// Creates or updates an existing user attendance record to <see cref="AttendanceStatus.Absent"></see>
        /// </summary>
        /// <param name="userId">The Id of the User to mark absent</param>
        /// <param name="moduleId">The Id of the Module. This is required if the Event has modules</param>
        /// <exception cref="EventArgumentException">
        /// Thrown when the userId parameter is 0 or out of range.
        /// </exception>
        /// <exception cref="EventInvalidOperationException">
        /// Thrown when the Event has modules, but no moduleId parameter is provided.
        /// </exception>
        public void MarkUserAbsent(int userId, int? moduleId = null)
        {
            if (userId == 0)
            {
                throw new EventArgumentException("Cannot mark User absent: parameter must not be 0.", nameof(userId));
            }
            if (_modules.Count > 0 && moduleId == null)
            {
                throw new EventInvalidOperationException("Cannot mark User absent: You must provide a module Id to update an attendance record for an Event that has modules.");
            }
            Attendance existingAttend = _attendance.FirstOrDefault(x => x.UserId == userId);
            if (existingAttend != null)
            {
                existingAttend.UpdateStatusAbsent();
            }
            else
            {
                Attendance newAttend = new Attendance(userId, moduleId, AttendanceStatus.Absent);
                _attendance.Add(newAttend);
            }
        }
        /// <summary>
        /// Creates or updates an existing user attendance record to <see cref="AttendanceStatus.Excused"></see>
        /// </summary>
        /// <param name="userId">The Id of the User to mark excused.</param>
        /// <param name="moduleId">The Id of the Module. This is required if the Event has modules</param>
        /// <exception cref="EventArgumentException">
        /// Thrown when the userId parameter is 0 or out of range.
        /// </exception>
        /// <exception cref="EventInvalidOperationException">
        /// Thrown when the Event has modules, but no moduleId parameter is provided.
        /// </exception>
        public void MarkUserExcused(int userId, int? moduleId = null)
        {
            if (userId == 0)
            {
                throw new EventArgumentException("Cannot mark User excused: parameter must not be 0.", nameof(userId));
            }
            if (_modules.Count > 0 && moduleId == null)
            {
                throw new EventInvalidOperationException("Cannot mark User excused: You must provide a module Id to update an attendance record for an Event that has modules.");
            }
            Attendance existingAttend = _attendance.FirstOrDefault(x => x.UserId == userId);
            if (existingAttend != null)
            {
                existingAttend.UpdateStatusExcused();
            }
            else
            {
                Attendance newAttend = new Attendance(userId, moduleId, AttendanceStatus.Excused);
                _attendance.Add(newAttend);
            }
        }
        /// <summary>
        /// Creates or updates an existing user attendance record to <see cref="AttendanceStatus.Pass"></see>
        /// </summary>
        /// <param name="userId">The Id of the User to mark Passed.</param>
        /// <param name="moduleId">The Id of the Module. This is required if the Event has modules</param>
        /// <param name="score">The score attained by the user on any exam associated with the Event/Module. This parameter will default to 100.0</param>
        /// <exception cref="EventArgumentException">
        /// Thrown when the userId parameter is 0 or out of range.
        /// </exception>
        /// <exception cref="EventInvalidOperationException">
        /// Thrown when the Event has modules, but no moduleId parameter is provided.
        /// </exception>
        public void MarkUserPass(int userId, int? moduleId = null, double score = 100.0)
        {
            if (userId == 0)
            {
                throw new EventArgumentException("Cannot mark User passed: parameter must not be 0.", nameof(userId));
            }
            if (_modules.Count > 0 && moduleId == null)
            {
                throw new EventInvalidOperationException("Cannot mark User passed: You must provide a module Id to update an attendance record for an Event that has modules.");
            }
            Attendance existingAttend = _attendance.FirstOrDefault(x => x.UserId == userId);
            if (existingAttend != null)
            {
                existingAttend.UpdateStatusPass(score);
            }
            else
            {
                Attendance newAttend = new Attendance(userId, moduleId, AttendanceStatus.Pass, score);
                _attendance.Add(newAttend);
            }
        }
        /// <summary>
        /// Creates or updates an existing user attendance record to <see cref="AttendanceStatus.Fail"></see>
        /// </summary>
        /// <param name="userId">The Id of the User to mark present</param>
        /// <param name="moduleId">The Id of the Module. This is required if the Event has modules</param>
        /// <param name="score">The score attained by the user on any exam associated with the Event/Module. This parameter will default to 0.0</param>
        /// <exception cref="EventArgumentException">
        /// Thrown when the userId parameter is 0 or out of range.
        /// </exception>
        /// <exception cref="EventInvalidOperationException">
        /// Thrown when the Event has modules, but no moduleId parameter is provided.
        /// </exception>
        public void MarkUserFail(int userId, int? moduleId = null, double score = 0.0)
        {
            if (userId == 0)
            {
                throw new EventArgumentException("Cannot mark User failed: parameter must not be 0.", nameof(userId));
            }
            if (_modules.Count > 0 && moduleId == null)
            {
                throw new EventInvalidOperationException("Cannot mark User failed: You must provide a module Id to update an attendance record for an Event that has modules.");
            }
            Attendance existingAttend = _attendance.FirstOrDefault(x => x.UserId == userId);
            if (existingAttend != null)
            {
                existingAttend.UpdateStatusFail(score);
            }
            else
            {
                Attendance newAttend = new Attendance(userId, moduleId, AttendanceStatus.Fail, score);
                _attendance.Add(newAttend);
            }
        }
        /// <summary>
        /// Creates a new <see cref="Registration">Registration</see> and adds it to the Event's Registrations collection.
        /// </summary>
        /// <param name="userId">The integer Id of the User.</param>
        /// <param name="userName">A string containing the Display Name of the User.</param>
        /// <param name="email">A string containing the User's email address.</param>
        /// <param name="contact">A string containing the User's primary contact phone number.</param>
        /// <param name="_dateTime">An implementation of <see cref="IDateTime"></see></param>
        /// <exception cref="EventArgumentException">
        /// Thrown when:
        /// <list type="bullet">
        ///     <item><description>Event is not accepting registrations</description></item>
        ///     <Item><description>When the UserId parameter is already registered for the Event</description></Item> 
        /// </list> 
        /// </exception>
        /// <exception cref="EventInvalidOperationException">
        /// Thrown when:
        /// <list type="bullet">
        ///     <item><description>The userId parameter is 0 or out of range</description></item>
        ///     <item><description>The userName parameter is empty/whitespace string</description></item>
        ///     <item><description>The email parameter is empty/whitespace string</description></item>
        /// </list>
        /// </exception>
        public void RegisterUser(int userId, string userName, string email, string contact, IDateTime _dateTime)
        {
            if (!IsAcceptingRegistrations(_dateTime))
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
            else if (_registrations.Exists(x => x.UserId == userId))
            {
                throw new EventInvalidOperationException("Cannot add Registration: User is already registered for this event.");
            }

            _registrations.Add(new Registration(userId, userName, email, contact, _dateTime));
        }
        /// <summary>
        /// Removes a Registration with the provided UserId from the Event's registration collection 
        /// </summary>
        /// <param name="userId">The integer ID for the User to whom the registration belongs</param>
        /// <exception cref="EventArgumentException">
        /// Thrown when no <see cref="Registration">Registration</see> for the given UserId was found in the Event's Registration collection.
        /// </exception>
        public void DeleteRegistrationByUserId(int userId)
        {
            var registrationForUser = _registrations.FirstOrDefault(x => x.UserId == userId);

            if (registrationForUser == null)
            {
                throw new EventArgumentException("Cannot remove registration: no registration for user id found.", nameof(userId));
            }
            _registrations.Remove(registrationForUser);
        }
        /// <summary>
        /// Updates the <see cref="RegistrationStatus"></see> of a Registration to "Accepted" by the UserId associated with the <see cref="Registration"></see>
        /// </summary>
        /// <param name="userId">The UserId of the <see cref="User"></see> associated with the <see cref="Registration"></see></param>
        /// <param name="_dateTime">An implementation of <see cref="IDateTime"></see></param>
        /// <exception cref="EventInvalidOperationException">
        /// Thrown when 
        /// <list type="bullet">
        /// <item><description>the <see cref="Event"></see> is expired.</description></item>
        /// <item><description>the <see cref="Event"></see> is at Maximum allowed "Accepted" registrations.</description></item>
        /// </list>
        /// </exception>
        /// <exception cref="EventArgumentException">Thrown when no Registration with the given UserId parameter could be found in the <see cref="_registrations"></see> collection.</exception>
        public void AcceptRegistrationByUserId(int userId, IDateTime _dateTime)
        {
            if (IsExpired(_dateTime))
            {
                throw new EventInvalidOperationException("Cannot accept registration: Event has expired.");
            }
            else if (CurrentAttendeesCount >= MaxRegistrations)
            {
                throw new EventInvalidOperationException("Cannot accept registration: Event is at Max Registrations");
            }
            var registrationForUserId = _registrations.FirstOrDefault(x => x.UserId == userId);
            if (registrationForUserId == null)
            {
                throw new EventArgumentException("Cannot accept registration: no registration with the given id was found.", nameof(userId));
            }
            registrationForUserId.UpdateStatusAccepted();
        }
        /// <summary>
        /// Updates the <see cref="RegistrationStatus"></see> of a Registration to "Accepted" by the RegistrationId associated with the <see cref="Registration"></see>
        /// </summary>
        /// <param name="regId">The RegistrationId of the <see cref="Registration"></see></param>
        /// <param name="_dateTime">An implementation of <see cref="IDateTime"></see></param>
        /// <exception cref="EventInvalidOperationException">
        /// Thrown when 
        /// <list type="bullet">
        /// <item><description>the <see cref="Event"></see> is expired.</description></item>
        /// <item><description>the <see cref="Event"></see> is at Maximum allowed "Accepted" registrations.</description></item>
        /// </list>
        /// </exception>
        /// <exception cref="EventArgumentException">Thrown when no Registration with the given UserId parameter could be found in the <see cref="_registrations"></see> collection.</exception>
        public void AcceptRegistrationByRegistrationId(int regId, IDateTime _dateTime)
        {
            if (IsExpired(_dateTime))
            {
                throw new EventInvalidOperationException("Cannot accept registration: Event has expired.");
            }
            else if (CurrentAttendeesCount >= MaxRegistrations)
            {
                throw new EventInvalidOperationException("Cannot accept registration: Event is at Max Registrations");
            }
            var registrationForId = _registrations.FirstOrDefault(x => x.Id == regId);
            if (registrationForId == null)
            {
                throw new EventArgumentException("Cannot accept registration: no registration with the given id was found.", nameof(regId));
            }
            registrationForId.UpdateStatusAccepted();
        }
        /// <summary>
        /// Updates the <see cref="RegistrationStatus"></see> of a Registration to "Rejected" by the id UserId associated with the <see cref="Registration"></see>
        /// </summary>
        /// <param name="userId">The UserId of the <see cref="User"></see> associated with the <see cref="Registration"></see></param>
        /// <param name="_dateTime">An implementation of <see cref="IDateTime"></see></param>
        /// <exception cref="EventInvalidOperationException">
        /// Thrown when the associated Event has expired.
        /// </exception>
        /// <exception cref="EventArgumentException">
        /// Thrown when no Registration with the provided UserId parameter was found in the <see cref="_registrations"> collection.</see>
        /// </exception>
        public void RejectRegistrationByUserId(int userId, IDateTime _dateTime)
        {
            if (IsExpired(_dateTime))
            {
                throw new EventInvalidOperationException("Cannot reject registration: Event has expired.");
            }
            else
            {
                var registrationForUserId = _registrations.FirstOrDefault(x => x.UserId == userId);
                if (registrationForUserId == null)
                {
                    throw new EventArgumentException("Cannot reject registration: no registration with the given id was found.", nameof(userId));
                }
                else
                {
                    registrationForUserId.UpdateStatusRejected();
                }
            }
        }
        /// <summary>
        /// Updates the <see cref="RegistrationStatus"></see> of a Registration to "Rejected" by the RegistrationId associated with the <see cref="Registration"></see>
        /// </summary>
        /// <param name="registrationId">The UserId of the <see cref="Registration"></see></param>
        /// <param name="_dateTime">An implementation of <see cref="IDateTime"></see></param>
        /// <exception cref="EventInvalidOperationException">
        /// Thrown when the associated Event has expired.
        /// </exception>
        /// <exception cref="EventArgumentException">
        /// Thrown when no Registration with the provided UserId parameter was found in the <see cref="_registrations"> collection.</see>
        /// </exception>
        public void RejectRegistrationByRegistrationId(int registrationId, IDateTime _dateTime)
        {
            if (IsExpired(_dateTime))
            {
                throw new EventInvalidOperationException("Cannot accept registration: Event has expired.");
            }
            else
            {
                var registrationForId = _registrations.FirstOrDefault(x => x.Id == registrationId);
                if (registrationForId == null)
                {
                    throw new EventArgumentException("Cannot reject registration: no registration with the given id was found.", nameof(registrationId));
                }
                else
                {
                    registrationForId.UpdateStatusRejected();
                }
            }
        }
        /// <summary>
        /// Updates the <see cref="RegistrationStatus"></see> of a Registration to "Standby" by the id UserId associated with the <see cref="Registration"></see>
        /// </summary>
        /// <param name="userId">The UserId of the <see cref="User"></see> associated with the <see cref="Registration"></see></param>
        /// <param name="_dateTime">An implementation of <see cref="IDateTime"></see></param>
        /// <exception cref="EventInvalidOperationException">
        /// Thrown when:
        /// <list type="bullet">
        /// <item><description>The associated Event has expired.</description></item>
        /// <item><description>The associated Event has reached its maximum number of Standby Registrations.</description></item>
        /// </list>
        /// </exception>
        /// <exception cref="EventArgumentException">
        /// Thrown when no Registration with the provided UserId parameter was found in the <see cref="_registrations"> collection.</see>
        /// </exception>
        public void StandbyRegistrationByUserId(int userId, IDateTime _dateTime)
        {
            if (IsExpired(_dateTime))
            {
                throw new EventInvalidOperationException("Cannot standby registration: Event has expired.");
            }
            else if (!IsStandByAvailable)
            {
                throw new EventInvalidOperationException("Cannot standby registration: Standby status not available for this Event.");
            }
            var registrationForUserId = _registrations.FirstOrDefault(x => x.UserId == userId);
            if (registrationForUserId == null)
            {
                throw new EventArgumentException("Cannot standby registration: no registration with the given id was found.", nameof(userId));
            }
            registrationForUserId.UpdateStatusStandby();
        }
    }
}
