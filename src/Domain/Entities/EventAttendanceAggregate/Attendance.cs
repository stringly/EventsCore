using EventsCore.Domain.Common;

namespace EventsCore.Domain.Entities.EventAttendanceAggregate
{
    /// <summary>
    /// Entity class that represents an attendance record for an Event or Event Module
    /// </summary>
    public class Attendance : BaseEntity
    {
        private Attendance() { }
        /// <summary>
        /// Creates a new instance of the class
        /// </summary>
        /// <param name="eventId">The Id of the Event associated with the Attendance record.</param>
        /// <param name="moduleId">The optional Id of the Module associated with the Attendance record.</param>
        /// <param name="userId">The Id of the User associated with the attendance record.</param>
        /// <param name="score">A double representing the score for any exam associated with the event/module.</param>
        /// <param name="status">An <see cref="AttendanceStatus"></see> enum with the desired status.</param>
        public Attendance(int eventId, int? moduleId, int userId, AttendanceStatus status = AttendanceStatus.Present, double score = 0.0)
        {            
            EventId = eventId;
            ModuleId = moduleId;
            UserId = userId;
            Score = score;
            Status = status;
        }
        /// <summary>
        /// The Id of the Event associated with the attendance record.
        /// </summary>
        public int EventId { get; private set; }
        /// <summary>
        /// The Id of the Event Module associated with the attendance record.
        /// </summary>
        public int? ModuleId { get; private set; }
        /// <summary>
        /// The Id of the User associated with the attendance record.
        /// </summary>
        public int UserId { get; private set; }
        /// <summary>
        /// The score the user attained for any examination associated with the Event/Module
        /// </summary>
        public double Score { get; private set; }
        /// <summary>
        /// The <see cref="AttendanceStatus"></see> Status of the Attendance Record.
        /// </summary>
        public AttendanceStatus Status { get; private set; }
        /// <summary>
        /// Updates the attendance record's status to <see cref="AttendanceStatus.Present"></see>
        /// </summary>
        public void UpdateStatusPresent()
        {
            Status = AttendanceStatus.Present;
        }
        /// <summary>
        /// Updates the attendance record's status to <see cref="AttendanceStatus.Absent"></see>
        /// </summary>
        public void UpdateStatusAbsent()
        {
            Status = AttendanceStatus.Absent;
        }
        /// <summary>
        /// Updates the attendance record's status to <see cref="AttendanceStatus.Excused"></see>
        /// </summary>
        public void UpdateStatusExcused()
        {
            Status = AttendanceStatus.Excused;
        }
        /// <summary>
        /// Updates the attendance record's status to <see cref="AttendanceStatus.Pass"></see>, and sets the record's score to the provided value.
        /// </summary>
        /// <param name="score">A double representing the score attained by the User for any exam associated with the Event/Module.</param>
        /// <remarks>
        /// If the "score" parameter is not provided, the score will default to 100.0.
        /// </remarks>
        public void UpdateStatusPass(double score = 100.0)
        {
            Status = AttendanceStatus.Pass;
            Score = score;
        }
        /// <summary>
        /// Updates the attendance record's status to <see cref="AttendanceStatus.Fail"></see>, and sets the record's score to the provided value.
        /// </summary>
        /// <param name="score">A doubld representing the score attained by the User for any exam associated with the Event/Module.</param>
        /// <remarks>
        /// If the "score" parameter is not provided, the score will default to 0.0
        /// </remarks>
        public void UpdateStatusFail(double score = 0.0)
        {
            Status = AttendanceStatus.Fail;
            Score = score;
        }

    }
}
