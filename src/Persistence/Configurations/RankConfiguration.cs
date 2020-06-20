using EventsCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsCore.Persistence.Configurations
{
    /// <summary>
    /// Implements <see cref="IEntityTypeConfiguration{TEntity}"></see> to configure the <see cref="Rank"></see> entity
    /// </summary>
    public class RankConfiguration : IEntityTypeConfiguration<Rank>
    {
        /// <summary>
        /// Configures the Entity
        /// </summary>
        /// <param name="builder"></param>
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
                .HasMaxLength(50);
        }
    }
}
