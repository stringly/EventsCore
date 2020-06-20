using EventsCore.Domain.Common;

namespace EventsCore.Domain.Entities
{
    /// <summary>
    /// Entity representing a <see cref="Entities.User"/> in a <see cref="Entities.UserRoleType"/>
    /// </summary>
    public class UserRole : BaseEntity
    {
        /// <summary>
        /// The Id of the <see cref="Entities.UserRoleType"/> record.
        /// </summary>
        public int UserRoleTypeId { get; set; }
        /// <summary>
        /// Navigation property to the <see cref="Entities.UserRoleType"/>
        /// </summary>
        public UserRoleType UserRoleType { get; set; }
        /// <summary>
        /// The Id of the <see cref="Entities.User"/>
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Navigation property to the <see cref="Entities.User"/>
        /// </summary>
        public User User { get; set; }
    }
}
