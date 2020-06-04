using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.EventAttendanceAggregate;
using System.Collections.Generic;
using System.Linq;

namespace EventsCore.Domain.Entities.EventAttendanceAggregate
{
    /// <summary>
    /// Aggregate root object that controls attendance entries for an Event and it's modules.
    /// </summary>
    public class EventAttendance : IAggregateRoot
    {
        /// <summary>
        /// Private, parameterless constructor for EF 
        /// </summary>
        private EventAttendance()
        {
        }
        /// <summary>
        /// Creates a new Instance of the EventAttendance Aggregate class
        /// </summary>
        /// <param name="eventId">The Id of the <see cref="Event"></see> associated with the aggregate</param>
        /// <param name="modules">A list of <see cref="Module"></see>s associated with the Event</param>
        /// <param name="attendance">A list of <see cref="Attendance"></see> records associated with the Event or it's modules.</param>
        public EventAttendance(int eventId, List<Module> modules, List<Attendance> attendance)
        {
            Id = eventId;
            _modules = modules;
            _attendance = attendance;
        }
        /// <summary>
        /// The Id of the <see cref="Event"></see> associated with the EventAttendance aggregate root.
        /// </summary>
        public int Id { get; private set; }

        private readonly List<Module> _modules;
        /// <summary>
        /// Returns a readonly list of the <see cref="Module"> associated with the Event.</see>
        /// </summary>
        public IEnumerable<Module> Modules => _modules.AsReadOnly();

        private readonly List<Attendance> _attendance;
        /// <summary>
        /// Returns a readonly list of <see cref="Attendance"></see> records associated with the Event.
        /// </summary>
        public IEnumerable<Attendance> Attendance => _attendance.AsReadOnly();

        /// <summary>
        /// Creates or updates an existing user attendance record to <see cref="AttendanceStatus.Present"></see>
        /// </summary>
        /// <param name="userId">The Id of the User to mark present</param>
        /// <param name="moduleId">The Id of the Module. This is required if the Event has modules</param>
        /// <exception cref="EventAttendanceAggregateArgumentException">
        /// Thrown when the userId parameter is 0 or out of range.
        /// </exception>
        /// <exception cref="EventAttendanceAggregateInvalidOperationException">
        /// Thrown when the Event has modules, but no moduleId parameter is provided.
        /// </exception>
        public void MarkUserPresent(int userId, int? moduleId = null)
        {
            if(userId == 0)
            {
                throw new EventAttendanceAggregateArgumentException("Cannot mark User present: parameter must not be 0.", nameof(userId));
            }
            if (_modules.Count > 0 && moduleId == null)
            {
                throw new EventAttendanceAggregateInvalidOperationException("Cannot mark User present: You must provide a module Id to update an attendance record for an Event that has modules.");
            }
            Attendance existingAttend = _attendance.FirstOrDefault(x => x.UserId == userId);
            if (existingAttend != null)
            {
                existingAttend.UpdateStatusPresent();
            }
            else
            {
                Attendance newAttend = new Attendance(Id, moduleId, userId);
                _attendance.Add(newAttend);
            }
            
        }
        /// <summary>
        /// Creates or updates an existing user attendance record to <see cref="AttendanceStatus.Absent"></see>
        /// </summary>
        /// <param name="userId">The Id of the User to mark absent</param>
        /// <param name="moduleId">The Id of the Module. This is required if the Event has modules</param>
        /// <exception cref="EventAttendanceAggregateArgumentException">
        /// Thrown when the userId parameter is 0 or out of range.
        /// </exception>
        /// <exception cref="EventAttendanceAggregateInvalidOperationException">
        /// Thrown when the Event has modules, but no moduleId parameter is provided.
        /// </exception>
        public void MarkUserAbsent(int userId, int? moduleId = null)
        {
            if (userId == 0)
            {
                throw new EventAttendanceAggregateArgumentException("Cannot mark User absent: parameter must not be 0.", nameof(userId));
            }
            if (_modules.Count > 0 && moduleId == null)
            {
                throw new EventAttendanceAggregateInvalidOperationException("Cannot mark User absent: You must provide a module Id to update an attendance record for an Event that has modules.");
            }
            Attendance existingAttend = _attendance.FirstOrDefault(x => x.UserId == userId);
            if (existingAttend != null)
            {
                existingAttend.UpdateStatusAbsent();
            }
            else
            {
                Attendance newAttend = new Attendance(Id, moduleId, userId, AttendanceStatus.Absent);
                _attendance.Add(newAttend);
            }
        }
        /// <summary>
        /// Creates or updates an existing user attendance record to <see cref="AttendanceStatus.Excused"></see>
        /// </summary>
        /// <param name="userId">The Id of the User to mark excused.</param>
        /// <param name="moduleId">The Id of the Module. This is required if the Event has modules</param>
        /// <exception cref="EventAttendanceAggregateArgumentException">
        /// Thrown when the userId parameter is 0 or out of range.
        /// </exception>
        /// <exception cref="EventAttendanceAggregateInvalidOperationException">
        /// Thrown when the Event has modules, but no moduleId parameter is provided.
        /// </exception>
        public void MarkUserExcused(int userId, int? moduleId = null)
        {
            if (userId == 0)
            {
                throw new EventAttendanceAggregateArgumentException("Cannot mark User excused: parameter must not be 0.", nameof(userId));
            }
            if (_modules.Count > 0 && moduleId == null)
            {
                throw new EventAttendanceAggregateInvalidOperationException("Cannot mark User excused: You must provide a module Id to update an attendance record for an Event that has modules.");
            }
            Attendance existingAttend = _attendance.FirstOrDefault(x => x.UserId == userId);
            if (existingAttend != null)
            {
                existingAttend.UpdateStatusExcused();
            }
            else
            {
                Attendance newAttend = new Attendance(Id, moduleId, userId, AttendanceStatus.Excused);
                _attendance.Add(newAttend);
            }
        }
        /// <summary>
        /// Creates or updates an existing user attendance record to <see cref="AttendanceStatus.Pass"></see>
        /// </summary>
        /// <param name="userId">The Id of the User to mark Passed.</param>
        /// <param name="moduleId">The Id of the Module. This is required if the Event has modules</param>
        /// <param name="score">The score attained by the user on any exam associated with the Event/Module. This parameter will default to 100.0</param>
        /// <exception cref="EventAttendanceAggregateArgumentException">
        /// Thrown when the userId parameter is 0 or out of range.
        /// </exception>
        /// <exception cref="EventAttendanceAggregateInvalidOperationException">
        /// Thrown when the Event has modules, but no moduleId parameter is provided.
        /// </exception>
        public void MarkUserPass(int userId, int? moduleId = null, double score = 100.0)
        {
            if (userId == 0)
            {
                throw new EventAttendanceAggregateArgumentException("Cannot mark User passed: parameter must not be 0.", nameof(userId));
            }
            if (_modules.Count > 0 && moduleId == null)
            {
                throw new EventAttendanceAggregateInvalidOperationException("Cannot mark User passed: You must provide a module Id to update an attendance record for an Event that has modules.");
            }
            Attendance existingAttend = _attendance.FirstOrDefault(x => x.UserId == userId);
            if (existingAttend != null)
            {
                existingAttend.UpdateStatusPass(score);
            }
            else
            {
                Attendance newAttend = new Attendance(Id, moduleId, userId, AttendanceStatus.Pass, score);
                _attendance.Add(newAttend);
            }
        }
        /// <summary>
        /// Creates or updates an existing user attendance record to <see cref="AttendanceStatus.Fail"></see>
        /// </summary>
        /// <param name="userId">The Id of the User to mark present</param>
        /// <param name="moduleId">The Id of the Module. This is required if the Event has modules</param>
        /// <param name="score">The score attained by the user on any exam associated with the Event/Module. This parameter will default to 0.0</param>
        /// <exception cref="EventAttendanceAggregateArgumentException">
        /// Thrown when the userId parameter is 0 or out of range.
        /// </exception>
        /// <exception cref="EventAttendanceAggregateInvalidOperationException">
        /// Thrown when the Event has modules, but no moduleId parameter is provided.
        /// </exception>
        public void MarkUserFail(int userId, int? moduleId = null, double score = 0.0)
        {
            if (userId == 0)
            {
                throw new EventAttendanceAggregateArgumentException("Cannot mark User failed: parameter must not be 0.", nameof(userId));
            }
            if (_modules.Count > 0 && moduleId == null)
            {
                throw new EventAttendanceAggregateInvalidOperationException("Cannot mark User failed: You must provide a module Id to update an attendance record for an Event that has modules.");
            }
            Attendance existingAttend = _attendance.FirstOrDefault(x => x.UserId == userId);
            if (existingAttend != null)
            {
                existingAttend.UpdateStatusFail(score);
            }
            else
            {
                Attendance newAttend = new Attendance(Id, moduleId, userId, AttendanceStatus.Fail, score);
                _attendance.Add(newAttend);
            }
        }
    }
}
