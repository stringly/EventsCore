using EventsCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography.X509Certificates;

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
            builder.ToTable("Events");
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
            builder.OwnsOne(a => a.Address);
            builder.HasOne(typeof(EventSeries), "EventSeries").WithMany();            
            builder.HasOne(u => u.Owner).WithMany(o => o.OwnedEvents).HasForeignKey(e => e.OwnerId);
            builder.OwnsOne(p => p.Rules);
            builder.HasMany<Registration>().WithOne().HasForeignKey("EventId");
            builder.HasMany<Attendance>().WithOne().HasForeignKey("EventId");
            builder.HasMany<Module>().WithOne().HasForeignKey("EventId");
            var navigation1 = builder.Metadata.FindNavigation(nameof(Event.Attendance));
            navigation1.SetPropertyAccessMode(PropertyAccessMode.Field);
            var navigation2 = builder.Metadata.FindNavigation(nameof(Event.Registrations));
            navigation2.SetPropertyAccessMode(PropertyAccessMode.Field);
            var navigation3 = builder.Metadata.FindNavigation(nameof(Event.Modules));
            navigation3.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
