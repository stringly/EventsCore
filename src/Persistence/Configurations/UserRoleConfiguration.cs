using EventsCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsCore.Persistence.Configurations
{
    /// <summary>
    /// Implements <see cref="IEntityTypeConfiguration{TEntity}"></see> to configure the <see cref="UserRole"></see> entity
    /// </summary>
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        /// <summary>
        /// Configures the entity.
        /// </summary>
        /// <param name="builder">An implementation of <see cref="EntityTypeBuilder{UserRole}"/></param>
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasOne(typeof(UserRoleType), "UserRoleType").WithMany();            
        }
    }
}
