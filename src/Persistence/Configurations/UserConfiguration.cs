using EventsCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsCore.Persistence.Configurations
{
    /// <summary>
    /// Implements <see cref="IEntityTypeConfiguration{TEntity}"></see> to configure the <see cref="User"></see> entity
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Configures the Entity
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.OwnsOne(p => p.NameFactory);
            builder.Property(p => p.LDAPName)
                .HasField("_LDAPName")
                .IsRequired()
                .HasMaxLength(25);
            builder.Property(p => p.BlueDeckId)
                .HasField("_blueDeckId")
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(p => p.IdNumber)
                .HasField("_idNumber")
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(p => p.Email)
                .HasField("_email")
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.ContactNumber)
                .HasField("_contactNumber")
                .HasMaxLength(10);
            builder.HasOne(typeof(Rank),"Rank").WithMany();

        }
    }
}
