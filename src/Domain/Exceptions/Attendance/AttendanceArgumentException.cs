using System;

namespace EventsCore.Domain.Exceptions.Attendance
{
    /// <summary>
    /// Exception used in <see cref="Attendance"></see> class methods.
    /// </summary>
    public class AttendanceArgumentException : ArgumentException
    {
        /// <summary>
        /// Creates a new Instance of the exception.
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        /// <param name="paramName">A string containing the name of the parameter that threw the exception.</param>
        public AttendanceArgumentException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
