using EventsCore.Application.Common.Interfaces;
using EventsCore.Domain.Common;
using EventsCore.Domain.Entities;
using EventsCore.Domain.Entities.EventAttendanceAggregate;
using EventsCore.Domain.Entities.EventModulesAggregate;
using EventsCore.Domain.Entities.EventRegistrationsAggregate;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Persistence
{
    public class EventsCoreDbContext: DbContext, IEventsCoreDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        public EventsCoreDbContext(DbContextOptions<EventsCoreDbContext> options) : base(options) { }

        public EventsCoreDbContext(
            DbContextOptions<EventsCoreDbContext> options, 
            IDateTime dateTime)
            : base(options)
        {
            _dateTime = dateTime;
        }

        public DbSet<EventAttendance> EventAttendance { get; set; }
        public DbSet<EventModules> EventModules { get; set; }
        public DbSet<EventRegistrations> EventRegistrations { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventSeries> EventSeries { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<User> Users { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventsCoreDbContext).Assembly);
        }
    }
}
