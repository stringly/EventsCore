using EventsCore.Domain.Entities;
using EventsCore.Domain.Entities.EventRegistrationsAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsCore.Persistence.Configurations
{
    /// <summary>
    /// Implements <see cref="IEntityTypeConfiguration{TEntity}"></see> to configure the <see cref="EventRegistrations"></see> entity
    /// </summary>
    public class EventRegistrationsConfiguration : IEntityTypeConfiguration<EventRegistrations>
    {
        /// <summary>
        /// Configures the Entity
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<EventRegistrations> builder)
        {
            builder.ToTable("Events");

            builder.OwnsOne(p => p.EventDates);
            builder.OwnsOne(p => p.Rules);

            var navigation1 = builder.Metadata.FindNavigation(nameof(EventRegistrations.Registrations));
            navigation1.SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.OwnsMany(r => r.Registrations, r => {
                r.WithOwner().HasForeignKey("EventId");
                r.Property(s => s.Status).HasConversion<string>();

            });
        }
    }
}
