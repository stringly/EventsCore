using System;

namespace EventsCore.Domain.Exceptions.EventRegistrationsAggregate
{
    /// <summary>
    /// Exception used in the <see cref="EventRegistrationsAggregate"></see> class
    /// </summary>
    public class EventRegistrationAggregateInvalidRegistrantException : ArgumentNullException
    {
        /// <summary>
        /// Creates a new instance of the exception.
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        /// <param name="paramName">A string containing the name of the parameter that threw the exception.</param>
        public EventRegistrationAggregateInvalidRegistrantException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
