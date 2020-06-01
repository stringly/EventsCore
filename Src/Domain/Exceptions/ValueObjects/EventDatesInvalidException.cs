using System;

namespace EventsCore.Domain.Exceptions.ValueObjects
{
    /// <summary>
    /// Exception class used in the <see cref="Domain.ValueObjects.EventDates"></see> value object.
    /// </summary>
    public class EventDatesInvalidException : Exception
    {
        /// <summary>
        /// Creates a new instance of the exception.
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        public EventDatesInvalidException(string message) : base(message: message) { } 
    }
}
