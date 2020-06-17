using System;

namespace EventsCore.Domain.Exceptions.Registration
{
    /// <summary>
    /// Exception used in <see cref="Registration"></see> class methods.
    /// </summary>
    public class RegistrationArgumentException : ArgumentException
    {
        /// <summary>
        /// Creates a new Instance of the exception.
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        /// <param name="paramName">A string containing the name of the parameter that threw the exception.</param>
        public RegistrationArgumentException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
