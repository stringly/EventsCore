namespace EventsCore.Domain.Entities.EventRegistrationsAggregate
{
    /// <summary>
    /// Enumeration representing the possible statuses for a <see cref="Registration"></see>
    /// </summary>
    public enum RegistrationStatus
    {
        /// <summary>
        /// Used when a registration is pending
        /// </summary>
        Pending,
        /// <summary>
        /// Used when a registration is accepted
        /// </summary>
        Accepted,
        /// <summary>
        /// Used when a registration is placed on standby
        /// </summary>        
        Standby,
        /// <summary>
        /// Used when a registration is rejected
        /// </summary>
        Rejected
    }
}
