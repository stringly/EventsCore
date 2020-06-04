using EventsCore.Domain.Entities;
using EventsCore.Domain.Entities.EventAttendanceAggregate;
using EventsCore.Domain.Entities.EventModulesAggregate;
using EventsCore.Domain.Entities.EventRegistrationsAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsCore.Persistence.Configurations
{
    /// <summary>
    /// Implements <see cref="IEntityTypeConfiguration{TEntity}"></see> to configure the <see cref="Domain.Entities.Event"></see> entity
    /// </summary>
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        /// <summary>
        /// Configures the Entity
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            //builder.Property(e => e.Id).HasColumnName("EventID");
            builder.Property(e => e.Title)
                .HasField("_title")
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.Description)
                .HasField("_description")
                .IsRequired()
                .HasColumnType("ntext");
            builder.HasOne(typeof(EventType), "EventType").WithMany();
            builder.OwnsOne(p => p.Dates);
            builder.HasOne(typeof(EventSeries), "EventSeries").WithMany();
            builder.OwnsOne(p => p.Rules);
            builder.HasOne(o => o.EventAttendance).WithOne()
                .HasForeignKey<EventAttendance>(o => o.Id);
            builder.HasOne(o => o.Registrations).WithOne()
                .HasForeignKey<EventRegistrations>(o => o.Id);
            builder.HasOne(o => o.Modules).WithOne()
                .HasForeignKey<EventModules>(o => o.Id);
        }
    }
}
