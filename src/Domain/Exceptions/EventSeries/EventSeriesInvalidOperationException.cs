using System;

namespace EventsCore.Domain.Exceptions.EventSeries
{
    /// <summary>
    /// Exception used in the <see cref="EventSeries"></see> class.
    /// </summary>
    public class EventSeriesInvalidOperationException : InvalidOperationException
    {
        /// <summary>
        /// Creates a new instance of the exception.
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        public EventSeriesInvalidOperationException(string message) : base(message: message) { }
    }
}
