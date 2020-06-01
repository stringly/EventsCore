using System;

namespace EventsCore.Domain.Exceptions.EventRegistrationsAggregate
{
    /// <summary>
    /// Exception used in the <see cref="EventRegistrationsAggregate"></see> class.
    /// </summary>
    public class EventRegistrationAggregateInvalidOperationException : InvalidOperationException
    {
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        public EventRegistrationAggregateInvalidOperationException(string message) : base(message: message) { }
    }
}
