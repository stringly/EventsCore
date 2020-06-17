using System;

namespace EventsCore.Domain.Exceptions.Attendance
{
    /// <summary>
    /// Exception used in <see cref="Event"></see> class methods.
    /// </summary>
    public class AttendanceInvalidOperationException : InvalidOperationException
    {
        /// <summary>
        /// Creates a new instance of the exception.
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        public AttendanceInvalidOperationException(string message) : base(message: message) { }
    }
}
