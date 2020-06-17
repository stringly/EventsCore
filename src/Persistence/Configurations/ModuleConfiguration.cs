using EventsCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsCore.Persistence.Configurations
{
    /// <summary>
    /// Implements <see cref="IEntityTypeConfiguration{TEntity}"></see> to configure the <see cref="Module"></see> entity
    /// </summary>
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        /// <summary>
        /// Configures the Entity
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.Property(x => x.Description)
                .HasField("_description");
            builder.Property(x => x.ModuleName)
                .HasField("_moduleName");
            var navigation1 = builder.Metadata.FindNavigation(nameof(Module.Attendance));
            navigation1.SetPropertyAccessMode(PropertyAccessMode.Field);
        }        
    }
}
