using System;

namespace EventsCore.Domain.Exceptions.EventType
{
    /// <summary>
    /// Exception class used in the <see cref="EventType"></see> class.
    /// </summary>
    public class EventTypeInvalidOperationException : InvalidOperationException
    {
        /// <summary>
        /// Creates a new instance of the exception.
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        public EventTypeInvalidOperationException(string message) : base(message: message) { }
    }
}
