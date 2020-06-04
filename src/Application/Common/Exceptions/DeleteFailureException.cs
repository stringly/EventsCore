using System;

namespace EventsCore.Application.Common.Exceptions
{
    /// <summary>
    /// Implementation of <see cref="Exception"></see> used in the Application namespace when a Delete operation fails.
    /// </summary>
    public class DeleteFailureException : Exception
    {
        /// <summary>
        /// Creates a new instance of the exception
        /// </summary>
        /// <param name="name">The Name of the Entity.</param>
        /// <param name="key">The key of the Entity.</param>
        /// <param name="message">A string containing the message.</param>
        public DeleteFailureException(string name, object key, string message) : base($"Deletion of entity \"{name}\" ({key} failed. {message}") { }
    }
}
