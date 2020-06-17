using EventsCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsCore.Persistence.Configurations
{
    /// <summary>
    /// Implements <see cref="IEntityTypeConfiguration{TEntity}"></see> to configure the <see cref="Registration"></see> entity
    /// </summary>
    public class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
    {
        /// <summary>
        /// Configures the Entity
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Registration> builder)
        {
            builder.Property(s => s.Status).HasConversion<string>();
        }
    }
}
