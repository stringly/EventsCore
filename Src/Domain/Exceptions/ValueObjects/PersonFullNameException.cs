using System;

namespace EventsCore.Domain.Exceptions.ValueObjects
{
    /// <summary>
    /// Exception class used in the <see cref="Domain.ValueObjects.PersonFullName"></see> value object.
    /// </summary>
    public class PersonFullNameException : Exception
    {
        /// <summary>
        /// Creates a new instance of the exception.
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        public PersonFullNameException(string message) : base(message) { }
    }
}
