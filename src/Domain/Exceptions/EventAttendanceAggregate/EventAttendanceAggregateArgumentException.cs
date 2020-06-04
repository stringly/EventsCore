using System;

namespace EventsCore.Domain.Exceptions.EventAttendanceAggregate
{
    /// <summary>
    /// Implementation of <see cref="ArgumentException"></see> used in the <see cref="EventAttendanceAggregate"></see> class.
    /// </summary>
    public class EventAttendanceAggregateArgumentException : ArgumentException
    {
        /// <summary>
        /// Creates a new instance of the exception
        /// </summary>
        /// <param name="message">A string containing the error message.</param>
        /// <param name="paramName">A string containing the name of the parameter that threw the exception.</param>
        public EventAttendanceAggregateArgumentException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
