using System;

namespace EventsCore.Domain.Common
{
    /// <summary>
    /// Abstract class for an Entity that needs audit information
    /// </summary>
    public abstract class AuditableEntity
    {
        /// <summary>
        /// The Id of the User who created the Entity.
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// The Timestamp for when the Entity was created.
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// The Id of the User who last modified the Entity.
        /// </summary>
        public int LastModifiedBy { get; set; }
        /// <summary>
        /// The Timestamp for when the Entity was last modified.
        /// </summary>
        public DateTime? LastModified { get; set; }
    }
}
