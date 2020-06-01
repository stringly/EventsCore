using Microsoft.EntityFrameworkCore;
using EventsCore.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.Common.Interfaces
{
    /// <summary>
    /// Interface 
    /// </summary>
    public interface EventsCoreDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
