using System;


namespace EventsCore.Domain.Exceptions.User
{
    /// <summary>
    /// Exception class used in the <see cref="User"></see> class.
    /// </summary>
    public class UserInvalidOperationException: InvalidOperationException
    {
        /// <summary>
        /// Creates a new instance of the exception.
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        public UserInvalidOperationException(string message) : base(message: message) { }
    }
}
