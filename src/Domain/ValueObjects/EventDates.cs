using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.ValueObjects;
using System;
using System.Collections.Generic;

namespace EventsCore.Domain.ValueObjects
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
        /// <exception cref="EventDatesInvalidException">
        /// Thrown when:
        /// <list type="bullet">
        /// <item><description>The evStart parameter is in the past.</description></item>
        /// <item><description>The evStart parameter is a date after the evEnd parameter in the past.</description></item>
        /// <item><description>The rgStart parameter is a date after the evStart parameter.</description></item>
        /// <item><description>The rgStart parameter is a date after the rgEnd parameter.</description></item>
        /// </list>
        /// </exception>
        public EventDates(DateTime evStart, DateTime evEnd, DateTime rgStart, DateTime rgEnd)
        {
            if (evStart > evEnd)
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
                throw new EventDatesInvalidException($"Event Registration Period Start Date cannot be after Registration Period End Date: Registration Period Start Date: {rgStart.ToString()} | Registration Period Start Date: {rgEnd.ToString()}");
            }
            else if (rgEnd > evStart)
            {
                // reject; event can't have registration period end after the event starts
                throw new EventDatesInvalidException($"Event Registration Period End Date cannot be after Event Start Date: Registration Period End Date: {rgEnd.ToString()} | Event Start Date: {rgEnd.ToString()}");
            }
            
            StartDate = evStart;
            EndDate = evEnd;
            RegistrationStartDate = rgStart;
            RegistrationEndDate = rgEnd;            
        }
        
        /// <summary>
        /// The Event's start date/time
        /// </summary>
        public DateTime StartDate { get; private set; }
        /// <summary>
        /// The Event's end date/time
        /// </summary>
        public DateTime EndDate { get; private set; }
        /// <summary>
        /// The Event's registration period start date
        /// </summary>
        public DateTime RegistrationStartDate { get; private set; }
        /// <summary>
        /// The Event's registration period end date
        /// </summary>
        public DateTime RegistrationEndDate { get; private set; }
        /// <summary>
        /// Enumerates the values in the object
        /// </summary>
        /// <returns>A <see cref="System.Collections.IEnumerable"></see> containing the values in the object.</returns>
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
