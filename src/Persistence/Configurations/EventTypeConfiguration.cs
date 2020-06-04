using EventsCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsCore.Persistence.Configurations
{
    /// <summary>
    /// Implements <see cref="IEntityTypeConfiguration{TEntity}"></see> to configure the <see cref="EventType"></see> entity
    /// </summary>
    public class EventTypeConfiguration : IEntityTypeConfiguration<EventType>
    {
        /// <summary>
        /// Configures the Entity
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<EventType> builder)
        {
            builder.Property(e => e.Id).HasColumnName("EventTypeID");
            builder.Property(e => e.Name)
                .HasField("_name")
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
