namespace EventsCore.Domain.Entities.EventAttendanceAggregate
{
    /// <summary>
    /// Enumeration representing the possible statuses for a <see cref="Attendance"></see> record.
    /// </summary>
    public enum AttendanceStatus
    {
        /// <summary>
        /// Attendee attended as required
        /// </summary>
        Present,
        /// <summary>
        /// Attendee did not attend as required
        /// </summary>
        Absent,
        /// <summary>
        /// Attendee was excused
        /// </summary>
        Excused,
        /// <summary>
        /// Attendee met requirements
        /// </summary>
        Pass,
        /// <summary>
        /// Attendee failed to meet requirements
        /// </summary>
        Fail,
    }
}
