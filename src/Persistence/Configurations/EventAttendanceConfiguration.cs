using EventsCore.Domain.Entities;
using EventsCore.Domain.Entities.EventAttendanceAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsCore.Persistence.Configurations
{
    public class EventAttendanceConfiguration : IEntityTypeConfiguration<EventAttendance>
    {
        public void Configure(EntityTypeBuilder<EventAttendance> builder)
        {
            builder.ToTable("Events");
            //builder.HasOne<Event>().WithOne().HasForeignKey<Event>(e => e.Id);
            
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
            //builder.OwnsMany(m => m.Modules, m =>
            //{
            //    m.WithOwner().HasForeignKey("Id");
            //});
        }
    }
}
