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
    /// <summary>
    /// An implementation of <see cref="IEventsCoreDbContext"></see> containing the Entities used in the EventsCore solution.
    /// </summary>
    public class EventsCoreDbContext: DbContext, IEventsCoreDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        /// <summary>
        /// Creates a new instance of the <see cref="EventsCoreDbContext"></see>
        /// </summary>
        /// <param name="options"></param>
        public EventsCoreDbContext(DbContextOptions<EventsCoreDbContext> options) : base(options) { }
        /// <summary>
        /// Creates a new instance of the <see cref="EventsCoreDbContext"></see>
        /// </summary>
        /// <param name="options"></param>
        /// <param name="dateTime"></param>
        public EventsCoreDbContext(
            DbContextOptions<EventsCoreDbContext> options, 
            IDateTime dateTime)
            : base(options)
        {
            _dateTime = dateTime;
        }
        /// <summary>
        /// A <see cref="DbSet{TEntity}"></see> of <see cref="Domain.Entities.EventAttendanceAggregate.EventAttendance"></see>
        /// </summary>
        public DbSet<EventAttendance> EventAttendance { get; set; }
        /// <summary>
        /// A <see cref="DbSet{TEntity}"></see> of <see cref="Domain.Entities.EventModulesAggregate.EventModules"></see>
        /// </summary>
        public DbSet<EventModules> EventModules { get; set; }
        /// <summary>
        /// A <see cref="DbSet{TEntity}"></see> of <see cref="Domain.Entities.EventRegistrationsAggregate.EventRegistrations"></see>
        /// </summary>
        public DbSet<EventRegistrations> EventRegistrations { get; set; }
        /// <summary>
        /// A <see cref="DbSet{TEntity}"></see> of <see cref="Event"></see>
        /// </summary>
        public DbSet<Event> Events { get; set; }
        /// <summary>
        /// A <see cref="DbSet{TEntity}"></see> of <see cref="Domain.Entities.EventSeries"></see>
        /// </summary>
        public DbSet<EventSeries> EventSeries { get; set; }
        /// <summary>
        /// A <see cref="DbSet{TEntity}"></see> of <see cref="EventType"></see>
        /// </summary>
        public DbSet<EventType> EventTypes { get; set; }
        /// <summary>
        /// A <see cref="DbSet{TEntity}"></see> of <see cref="Ranks"></see>
        /// </summary>
        public DbSet<Rank> Ranks { get; set; }
        /// <summary>
        /// A <see cref="DbSet{TEntity}"></see> of <see cref="Users"></see>
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Saves changes to the context.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Executes instructions when the model is created.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventsCoreDbContext).Assembly);
        }
    }
}
