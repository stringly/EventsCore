using System;
using System.Collections.Generic;
using System.Text;

namespace EventsCore.Domain.Exceptions.Registration
{
    /// <summary>
    /// Exception used in <see cref="Registration"></see> class methods.
    /// </summary>
    public class RegistrationInvalidOperationException : InvalidOperationException
    {
        /// <summary>
        /// Creates a new instance of the exception.
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        public RegistrationInvalidOperationException(string message) : base(message: message) { }

    }
}
