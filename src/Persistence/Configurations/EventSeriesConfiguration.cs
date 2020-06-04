using EventsCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsCore.Persistence.Configurations
{
    public class EventSeriesConfiguration : IEntityTypeConfiguration<EventSeries>
    {
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
