using EventsCore.Domain.Entities.EventAttendanceAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsCore.Persistence.Configurations
{
    /// <summary>
    /// Implements <see cref="IEntityTypeConfiguration{TEntity}"></see> to configure the <see cref="EventAttendance"></see> entity
    /// </summary>
    public class EventAttendanceConfiguration : IEntityTypeConfiguration<EventAttendance>
    {
        /// <summary>
        /// Configures the Entity
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<EventAttendance> builder)
        {
            builder.ToTable("Events");
            
            var navigation1 = builder.Metadata.FindNavigation(nameof(EventAttendance.Attendance));
            navigation1.SetPropertyAccessMode(PropertyAccessMode.Field);
            
            builder.OwnsMany(a => a.Attendance, a =>
            {
                a.WithOwner().HasForeignKey("EventId");
                a.Property(s => s.Status).HasConversion<string>();                
            });
            var navigation2 = builder.Metadata.FindNavigation(nameof(EventAttendance.Modules));
            navigation2.SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.HasMany(x => x.Modules).WithOne().HasForeignKey("Id");
        }
    }
}
