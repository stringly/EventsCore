using Microsoft.EntityFrameworkCore;
using EventsCore.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace NetCore.Application.Common.Interfaces
{
    public interface EventsCoreDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
