using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.ValueObjects;
using System;
using System.Collections.Generic;

namespace EventsCore.Domain.Entities.ValueObjects
{
    /// <summary>
    /// Value Object class that stores a collection of dates for an Event
    /// </summary>
    /// <remarks>
    /// This object contains the Start/End Dates for an Event, as well as the Registration Period Start/End Dates. These are encapsulated in this object so that the dates can be validated.
    /// </remarks>
    public class EventDates : ValueObject
    {
        /// <summary>
        /// Private, parameterless constructor implemented for EF purposes. This object cannot be created with no parameters.
        /// </summary>
        private EventDates()
        {
        }
        /// <summary>
        /// Constructor for the EventDates argument.
        /// </summary>
        /// <remarks>
        /// The constructor enforces the rule that an Event cannot have a Start Date for any date in the past. This check is performed against the System time. Keep this requirement in mind for testing.
        /// </remarks>
        /// <param name="evStart">DateTime object representing the Date/Time the Event is to begin. Must be before End Date.</param>
        /// <param name="evEnd">DateTime object representing the Date/Time the Event is to end. Must be after Start Date.</param>
        /// <param name="rgStart">DateTime object representing the Date/Time the Registration Period for the Event is to begin. Must be before Event Start Date.</param>
        /// <param name="rgEnd">DateTime object representing the Date/Time the Registration Period for the Event is to end. Must be before Event Start Date and after Registration Start Date.</param>        
        public EventDates(DateTime evStart, DateTime evEnd, DateTime rgStart, DateTime rgEnd, IDateTime dateTimeProvider)
        {
            _dateTime = dateTimeProvider;
            if (evStart < _dateTime.Now)
            {
                // reject; event can't start in the past
                throw new EventDatesInvalidException($"Event Start Date cannot be in the past: Start Date: {evStart.ToString()} | System Date: {_dateTime.Now.ToString()}");
            }
            else if (evStart > evEnd)
            {
                // reject; event can't end before it starts
                throw new EventDatesInvalidException($"Event Start Date cannot be after Event End Date: Start Date: {evStart.ToString()} | End Date: {evEnd.ToString()}");
            }
            else if (rgStart > evStart)
            {
                // reject; event can't have registration period start after event start
                throw new EventDatesInvalidException($"Event Registration Period Start Date cannot be after Event Start Date: Registration Period Start Date: {rgStart.ToString()} | Event Start Date: {evStart.ToString()}");
            }
            else if (rgStart > rgEnd)
            {
                // reject; event can't have registration period end before it starts
                throw new EventDatesInvalidException($"Event Registration Period Start Date cannot be after Registration Period End Date: Registration Period Start Date: {rgStart.ToString()} | Event Start Date: {rgEnd.ToString()}");
            }
            
            StartDate = evStart;
            EndDate = evEnd;
            RegistrationStartDate = rgStart;
            RegistrationEndDate = rgEnd;            
        }
        private readonly IDateTime _dateTime;
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime RegistrationStartDate { get; private set; }
        public DateTime RegistrationEndDate { get; private set; }
        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return StartDate;
            yield return EndDate;
            yield return RegistrationStartDate;
            yield return RegistrationEndDate;
        }
    }
}
