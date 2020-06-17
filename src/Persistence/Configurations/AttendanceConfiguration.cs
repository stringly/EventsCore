using EventsCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsCore.Persistence.Configurations
{
    /// <summary>
    /// Implements <see cref="IEntityTypeConfiguration{TEntity}"></see> to configure the <see cref="Attendance"></see> entity
    /// </summary>
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        /// <summary>
        /// Configures the Entity
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.Property(s => s.Status).HasConversion<string>();
        }
    }
}
