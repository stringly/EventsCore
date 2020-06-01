using System;

namespace EventsCore.Domain.Exceptions.Rank
{
    /// <summary>
    /// Exception class that is used in the <see cref="Rank"></see> class.
    /// </summary>
    public class RankInvalidOperationException : InvalidOperationException
    {
        /// <summary>
        /// Creates a new instance of the exception.
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        public RankInvalidOperationException(string message) : base(message: message) { }
    }
}
