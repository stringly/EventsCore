using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.ValueObjects;
using System.Collections.Generic;

namespace EventsCore.Domain.ValueObjects
{
    /// <summary>
    /// Class that creates and stores Event Registration rules for an Event.
    /// </summary>
    /// <remarks>
    /// This class contains the following properties:
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// MaxRegistrations: An unsigned integer number representing the Maximum Registrations allowed for the Event. This property is always required.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// MinRegistrations: An unsigned integer number representing the minimum Registrations required for the Event. This property is optional, and defaults to 1.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// MaxStandbyRegistrations: An unsigned integer number representing the Maximum number of Standby Registrations allowed for the Event. This property is optional and defaults to zero.
    /// </description>
    /// </item>
    /// </list>
    /// </remarks>
    public class EventRegistrationRules : ValueObject
    {
        /// <summary>
        /// Private, parameterless constructor; implemented for EF purposes. This object cannot be created with no paramaters.
        /// </summary>
        private EventRegistrationRules() { }
        /// <summary>
        /// Creates a new EventRegistrationRules object with the provided Maximum Registration count.
        /// </summary>
        /// <param name="maxRegistrations">An unsigned, non-zero integer representing the Maximum number of Registrations allowed for this Event.</param>
        /// <exception cref="EventRegistrationRulesArgumentException">Thrown when the maxRegistrations parameter is less than 1.</exception>
        public EventRegistrationRules(uint maxRegistrations)
        {
            // uint will prevent negative integers, but zero still requires a guard
            if (maxRegistrations < 1)
            {
                throw new EventRegistrationRulesArgumentException("Cannot create Event Ruleset: parameter must be greater than 0", nameof(maxRegistrations));
            } 
            MaxRegistrations = maxRegistrations;
            MinRegistrations = 1;
            MaxStandbyRegistrations = 0;
        }
        /// <summary>
        /// Creates a new EventRegistrationRules object with the provided Maximum and Minimum Registration count.
        /// </summary>
        /// <param name="maxRegistrations">An unsigned, non-zero integer representing the Maximum number of Registrations allowed for this Event.</param>
        /// <param name="minRegistrations">An unsigned integer representing the Minimum number of Registrations required for this event. This value must be less than or equal to the maxRegistrations parameter.</param>
        /// <exception cref="EventRegistrationRulesArgumentException">
        /// Thrown when:
        /// <list type="bullet">
        /// <item><description>The maxRegistrations parameter is less than 1.</description></item>
        /// <item><description>The maxRegistrations parameter is less than the minRegistrations parameter.</description></item>
        /// </list>
        /// </exception>
        public EventRegistrationRules(uint maxRegistrations, uint minRegistrations)
        {
            if (maxRegistrations < 1)
            {
                throw new EventRegistrationRulesArgumentException("Cannot create Event Ruleset: parameter must be greater than 0", nameof(maxRegistrations));
            }
            else if(minRegistrations > maxRegistrations)
            {
                throw new EventRegistrationRulesArgumentException("Cannot create Registration ruleset: maxRegistration parameter must be greater than minRegistration", nameof(minRegistrations));
            }
            MaxRegistrations = maxRegistrations;
            MinRegistrations = minRegistrations;
            MaxStandbyRegistrations = 0;
        }

        /// <summary>
        /// Creates a new EventRegistrationRules object with the provided Maximum and Minimum Registration count and the provided number of Standby Registrations allowed.
        /// </summary>
        /// <param name="maxRegistrations">An unsigned, non-zero integer representing the Maximum number of Registrations allowed for this Event.</param>
        /// <param name="minRegistrations">An unsigned integer representing the Minimum number of Registrations required for this event. This value must be less than or equal to the maxRegistrations parameter.</param>
        /// <param name="maxStandbyRegistrations">An unsigned integer representing the Maximum number of Standy Registrations allowed for this event. If this is set to zero, no Standby Registrations will be allowed.</param>
        /// <exception cref="EventRegistrationRulesArgumentException">
        /// Thrown when:
        /// <list type="bullet">
        /// <item><description>The maxRegistrations parameter is less than 1.</description></item>
        /// <item><description>The maxRegistrations parameter is less than the minRegistrations parameter.</description></item>
        /// </list>
        /// </exception>
        public EventRegistrationRules(uint maxRegistrations, uint minRegistrations, uint maxStandbyRegistrations)
        {
            if(maxRegistrations == 0)
            {
                throw new EventRegistrationRulesArgumentException("Cannot create Event Ruleset: parameter must be greater than 0", nameof(maxRegistrations));
            }
            else if(minRegistrations > maxRegistrations)
            {
                throw new EventRegistrationRulesArgumentException("Cannot create Registration ruleset: maxRegistration parameter must be greater than minRegistration", nameof(minRegistrations));
            }
            MaxRegistrations = maxRegistrations;
            MinRegistrations = minRegistrations;
            MaxStandbyRegistrations = maxStandbyRegistrations;
        }
        /// <summary>
        /// The Maximum number of Registrations allowed.
        /// </summary>
        public uint MaxRegistrations { get; private set; }
        /// <summary>
        /// The Minimum number of Registrations permitted. 
        /// </summary>
        /// <remarks>
        /// If no Minimum Registrations value is provided by constructor, this property defaults to 1.
        /// </remarks>
        public uint MinRegistrations { get; private set; }
        /// <summary>
        /// The Maximum number of Standby Registrations allowed. 
        /// </summary>
        /// <remarks>
        /// If no Maximum number of Standby Registrations is provided by constructor, this defaults to 0 and standby Registrations are prohibited.
        /// </remarks>
        public uint MaxStandbyRegistrations { get; private set; }
        /// <summary>
        /// Enumerates the values in the object.
        /// </summary>
        /// <returns>An <see cref="System.Collections.IEnumerable"></see> containing the values in the object.</returns>
        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return MaxRegistrations;
            yield return MinRegistrations;
            yield return MaxStandbyRegistrations;
        }
    }
}
