using EventsCore.Domain.Common;

namespace EventsCore.Domain.Entities
{
    /// <summary>
    /// Entity representing an application role for a <see cref="User"/>
    /// </summary>
    public class UserRoleType : BaseEntity
    {
        /// <summary>
        /// A string containing the name of the Role.
        /// </summary>
        public string Name { get; set; }
    }
}
