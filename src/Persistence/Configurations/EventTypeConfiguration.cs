using EventsCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsCore.Persistence.Configurations
{
    public class EventTypeConfiguration : IEntityTypeConfiguration<EventType>
    {
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
