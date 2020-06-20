using EventsCore.Application.Common.Interfaces;
using EventsCore.Common;
using EventsCore.Domain.Common;
using EventsCore.Domain.Entities;
using EventsCore.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        /// <param name="options">An implementation of <see cref="DbContextOptions{EventsCoreDbContext}"/></param>
        /// <param name="currentUserService">An implementation of <see cref="ICurrentUserService"/></param>
        /// <param name="dateTime">An implementation of <see cref="IDateTime"/></param>
        public EventsCoreDbContext(
            DbContextOptions<EventsCoreDbContext> options, 
            ICurrentUserService currentUserService,
            IDateTime dateTime)
            : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;

        }
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
        /// A <see cref="DbSet{TEntity}"/> of <see cref="UserRoleType"/>
        /// </summary>
        public DbSet<UserRoleType> UserRoleTypes { get;set;}
        /// <summary>
        /// Saves changes to the context.
        /// </summary>
        /// <remarks>
        /// This override handles writing metadata to added/updated <see cref="AuditableEntity"/> entities and owner info when <see cref="Event"/> entities are added.
        /// </remarks>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                var userId = Users.FirstOrDefault(x => x.LDAPName == _currentUserService.UserId)?.Id ?? 0;               
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = userId;
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = userId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }
            foreach (var entry in ChangeTracker.Entries<Event>())
            {
                
                switch (entry.State)
                {
                    case EntityState.Added:
                        var userId = Users.FirstOrDefault(x => x.LDAPName == _currentUserService.UserId)?.Id ?? 0;
                        entry.Entity.UpdateOwner(userId);                        
                        break;                    
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        /// <summary>
        /// Override that saves changes
        /// </summary>
        /// <remarks>
        /// This override handles writing metadata to added/updated <see cref="AuditableEntity"/> entities and owner info when <see cref="Event"/> entities are added.
        /// </remarks>
        /// <returns></returns>
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                var userId = Users.FirstOrDefault(x => x.LDAPName == _currentUserService.UserId)?.Id ?? 0;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = userId;
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = userId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }
            foreach (var entry in ChangeTracker.Entries<Event>())
            {

                switch (entry.State)
                {
                    case EntityState.Added:
                        var userId = Users.FirstOrDefault(x => x.LDAPName == _currentUserService.UserId)?.Id ?? 0;
                        entry.Entity.UpdateOwner(userId);
                        break;
                }
            }
            return base.SaveChanges();
        }
        /// <summary>
        /// Executes instructions when the model is created.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventsCoreDbContext).Assembly);
            modelBuilder.Entity<Rank>().HasData(                
                new { Id = 1, FullName = "Police Officer", Abbreviation = "P/O" },
                new { Id = 2, FullName = "Police Officer First Class", Abbreviation = "POFC" },
                new { Id = 3, FullName = "Corporal", Abbreviation = "Cpl." },
                new { Id = 4, FullName = "Sergeant", Abbreviation = "Sgt." },
                new { Id = 5, FullName = "Lieutenant", Abbreviation = "Lt." },
                new { Id = 6, FullName = "Captain", Abbreviation = "Capt." },
                new { Id = 7, FullName = "Major", Abbreviation = "Maj." },
                new { Id = 8, FullName = "Deputy Chief", Abbreviation = "D/Chief" },
                new { Id = 9, FullName = "Assistant Chief", Abbreviation = "A/Chief" },
                new { Id = 10, FullName = "Chief of Police", Abbreviation = "Chief" }
                );
            modelBuilder.Entity<EventType>().HasData(
                new { Id = 1, Name = "Training" },
                new { Id = 2, Name = "Overtime" },
                new { Id = 3, Name = "Assignment" }                
                );
            modelBuilder.Entity<User>(u =>
                {
                    u.HasData(new { Id = 1, LDAPName = "jcs30", BlueDeckId = (uint)1, IdNumber = "3134", Email = "jcs3082@hotmail.com", ContactNumber = "3016483444", RankId = 5 });
                    u.OwnsOne(n => n.NameFactory).HasData(new { UserId = 1, First = "Jason", Last = "Smith" });
                });
            modelBuilder.Entity<UserRoleType>().HasData(
                    new { Id = 1, Name = "Administrator" }
                );
            modelBuilder.Entity<UserRole>().HasData(
                    new { Id = 1, UserId = 1, UserRoleTypeId = 1 }
                );
                
        }
    }
}
