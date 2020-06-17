using EventsCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.Common.Interfaces
{
    /// <summary>
    /// Interface that set defines the contract for the DbContext used in the application
    /// </summary>
    public interface IEventsCoreDbContext
    {
        /// <summary>
        /// A <see cref="DbSet{Events}"></see>
        /// </summary>
        DbSet<Event> Events { get; set; }
        /// <summary>
        /// A <see cref="DbSet{EventSeries}"></see>
        /// </summary>
        DbSet<EventSeries> EventSeries { get;set;}
        /// <summary>
        /// A <see cref="DbSet{EventType}"></see>
        /// </summary>
        DbSet<EventType> EventTypes { get;set;}
        /// <summary>
        /// A <see cref="DbSet{Ranks}"></see>
        /// </summary>
        DbSet<Rank> Ranks { get; set; }
        /// <summary>
        /// A <see cref="DbSet{Users}"></see>
        /// </summary>
        DbSet<User> Users { get; set; }
        /// <summary>
        /// Saves the changes to the Context
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"></see></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
