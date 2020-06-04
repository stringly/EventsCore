using EventsCore.Domain.Entities.EventModulesAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsCore.Persistence.Configurations
{
    /// <summary>
    /// Implements <see cref="IEntityTypeConfiguration{TEntity}"></see> to configure the <see cref="EventModules"></see> entity
    /// </summary>
    public class EventModulesConfiguration : IEntityTypeConfiguration<EventModules>
    {
        /// <summary>
        /// Configures the Entity
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<EventModules> builder)
        {
            builder.ToTable("Events");
            //builder.HasOne<Event>().WithOne().HasForeignKey<Event>(e => e.Id);
            
            var navigation1 = builder.Metadata.FindNavigation(nameof(EventModules.Modules));
            navigation1.SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.HasMany(x => x.Modules).WithOne().HasForeignKey("EventId");

        }
    }
}
