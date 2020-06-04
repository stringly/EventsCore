using EventsCore.Domain.Entities;
using EventsCore.Domain.Entities.EventModulesAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsCore.Persistence.Configurations
{
    public class EventModulesConfiguration : IEntityTypeConfiguration<EventModules>
    {
        public void Configure(EntityTypeBuilder<EventModules> builder)
        {
            builder.ToTable("Events");
            //builder.HasOne<Event>().WithOne().HasForeignKey<Event>(e => e.Id);
            
            var navigation1 = builder.Metadata.FindNavigation(nameof(EventModules.Modules));
            navigation1.SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.HasMany(x => x.Modules).WithOne().HasForeignKey("EventId");

            //builder.HasMany(m => m.Modules, m => {                
            //    m.WithOwner().HasForeignKey("EventId");
            //    m.Property(x => x.Description)
            //        .HasField("_description");
            //    m.Property(x => x.ModuleName)
            //        .HasField("_moduleName");
            //});
        }
    }
}
