using EventsCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsCore.Persistence.Configurations
{
    /// <summary>
    /// Implements <see cref="IEntityTypeConfiguration{TEntity}"></see> to configure the <see cref="EventSeries"></see> entity
    /// </summary>
    public class EventSeriesConfiguration : IEntityTypeConfiguration<EventSeries>
    {
        /// <summary>
        /// Configures the Entity
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<EventSeries> builder)
        {
            builder.Property(e => e.Id).HasColumnName("EventSeriesID");
            builder.Property(e => e.Title)
                .HasField("_title")
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.Description)
                .HasField("_description")
                .IsRequired();
        }
    }
}
