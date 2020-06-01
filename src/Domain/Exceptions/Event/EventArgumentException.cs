using System;

namespace EventsCore.Domain.Exceptions.Event
{
    /// <summary>
    /// Exception used in <see cref="Event"></see> class methods.
    /// </summary>
    public class EventArgumentException : ArgumentException
    {
        /// <summary>
        /// Creates a new Instance of the exception.
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        /// <param name="paramName">A string containing the name of the parameter that threw the exception.</param>
        public EventArgumentException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
