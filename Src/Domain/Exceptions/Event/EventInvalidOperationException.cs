using System;

namespace EventsCore.Domain.Exceptions.Event
{
    /// <summary>
    /// Exception used in <see cref="Event"></see> class methods.
    /// </summary>
    public class EventInvalidOperationException : InvalidOperationException
    {
        /// <summary>
        /// Creates a new instance of the exception.
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        public EventInvalidOperationException(string message) : base(message: message) { }

    }
}
