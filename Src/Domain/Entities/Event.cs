using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions;
using EventsCore.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsCore.Domain.Entities
{
    /// <summary>
    /// Domain Entity that represents an Event for which Users can register.
    /// </summary>
    public class Event : IEntity
    {
        private Event() { }
        /// <summary>
        /// Creates a new Event object
        /// </summary>
        /// <param name="title">A string containing the Event's Title. Must not be null or only whitespace.</param>
        /// <param name="description">A string containing the Event's Description. Must not be null or only whitespace.</param>
        /// <param name="dates">An </param>
        public Event(string title, string description, EventDates dates)
        {
            UpdateTitle(title);
            UpdateDescription(description);
            UpdateEventDates(dates);
            
        }

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
    }
}
