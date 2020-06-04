using EventsCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsCore.Persistence.Configurations
{
    public class RankConfiguration : IEntityTypeConfiguration<Rank>
    {
        public void Configure(EntityTypeBuilder<Rank> builder)
        {
            builder.Property(e => e.Id).HasColumnName("RankID");
            builder.Property(e => e.Abbreviation)
                .HasField("_abbreviation")
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(e => e.FullName)
                .HasField("_fullName")
                .IsRequired()
                .HasMaxLength(25);
        }
    }
}
