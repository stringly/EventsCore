using System;

namespace EventsCore.Domain.Exceptions.EventModulesAggregate
{
    /// <summary>
    /// Exception class used in the <see cref="EventModulesAggregate"></see> class.
    /// </summary>
    public class EventModulesAggregateArgumentException : ArgumentException
    {
        /// <summary>
        /// Creates a new instance of the exception.
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        /// <param name="paramName">A string containing the name of the parameter that threw the exception.</param>
        public EventModulesAggregateArgumentException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
