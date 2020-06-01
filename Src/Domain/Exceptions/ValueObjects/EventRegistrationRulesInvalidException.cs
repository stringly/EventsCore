using System;

namespace EventsCore.Domain.Exceptions.ValueObjects
{
    /// <summary>
    /// Exception class used in the <see cref="Domain.ValueObjects.EventRegistrationRules"></see> value object.
    /// </summary>
    public class EventRegistrationRulesArgumentException : ArgumentException
    {
        /// <summary>
        /// Creates a new instance of the exception.
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        /// <param name="paramName">A string containing the name of the parameter that threw the exception.</param>
        public EventRegistrationRulesArgumentException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
