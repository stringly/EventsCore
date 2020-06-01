using System;

namespace EventsCore.Domain.Exceptions.ValueObjects
{
    /// <summary>
    /// Exception class used in the <see cref="Domain.ValueObjects.Address"></see> value object.
    /// </summary>
    public class AddressInvalidException : Exception
    {
        /// <summary>
        /// Creates a new instance of the exception.
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        public AddressInvalidException(string message) : base(message) { }
    }
}
