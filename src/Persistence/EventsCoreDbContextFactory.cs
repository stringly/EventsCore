using Microsoft.EntityFrameworkCore;

namespace EventsCore.Persistence
{
    public class EventsCoreDbContextFactory : DesignTimeDbContextFactoryBase<EventsCoreDbContext>    
    {
        protected override EventsCoreDbContext CreateNewInstance(DbContextOptions<EventsCoreDbContext> options)
        {
            return new EventsCoreDbContext(options);
        }
    }
}
