using Microsoft.EntityFrameworkCore;

namespace EventsCore.Persistence
{
    /// <summary>
    /// Implemention of <see cref="DesignTimeDbContextFactoryBase{TContext}"></see>
    /// </summary>
    public class EventsCoreDbContextFactory : DesignTimeDbContextFactoryBase<EventsCoreDbContext>    
    {
        /// <summary>
        /// Creates a new instance of the <see cref="EventsCoreDbContext"></see>
        /// </summary>
        /// <param name="options">A <see cref="DbContextOptions"></see> object.</param>
        /// <returns></returns>
        protected override EventsCoreDbContext CreateNewInstance(DbContextOptions<EventsCoreDbContext> options)
        {
            return new EventsCoreDbContext(options);
        }
    }
}
