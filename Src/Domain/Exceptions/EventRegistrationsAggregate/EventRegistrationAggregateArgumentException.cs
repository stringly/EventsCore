using System;

namespace EventsCore.Domain.Exceptions.EventRegistrationsAggregate
{
    /// <summary>
    /// Exception class used in the <see cref="EventRegistrationsAggregate"></see> class.
    /// </summary>
    public class EventRegistrationAggregateArgumentException : ArgumentException
    {
        /// <summary>
        /// Creates a new instance of the excepition
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        /// <param name="paramName">A string containing the name of the parameter that threw the exception.</param>
        public EventRegistrationAggregateArgumentException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
