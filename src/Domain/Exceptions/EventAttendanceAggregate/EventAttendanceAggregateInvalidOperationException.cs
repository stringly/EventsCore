using System;

namespace EventsCore.Domain.Exceptions.EventAttendanceAggregate
{
    /// <summary>
    /// Implementation of <see cref="InvalidOperationException"></see> used in the <see cref="EventAttendanceAggregate"></see> class.
    /// </summary>
    public class EventAttendanceAggregateInvalidOperationException : InvalidOperationException
    {
        /// <summary>
        /// Creates a new instance of the exception
        /// </summary>
        /// <param name="message">A string containing the error message.</param>
        public EventAttendanceAggregateInvalidOperationException(string message) : base(message: message) { }
    }
}
