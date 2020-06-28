using EventsCore.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.Events.Queries.GetCanEditEvent
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"/> that is used to determine if a User is authorized to make changes to an <see cref="Domain.Entities.Event"/>
    /// </summary>
    public class GetCanEditEventQueryHandler : IRequestHandler<GetCanEditEventQuery, bool>
    {
        private readonly IEventsCoreDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        /// <summary>
        /// Creates a new instance of the handler
        /// </summary>
        /// <param name="context">An implementation of <see cref="IEventsCoreDbContext"/></param>
        /// <param name="currentUserService">An implementation of <see cref="ICurrentUserService"/></param>
        public GetCanEditEventQueryHandler(IEventsCoreDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }
        /// <summary>
        /// Handles the request.
        /// </summary>
        /// <param name="request">A <see cref="GetCanEditEventQuery"/> request object.</param>
        /// <param name="cancellationToken">A cancellationtoken.</param>
        /// <returns></returns>
        public async Task<bool> Handle(GetCanEditEventQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(x => x.Roles)
                    .ThenInclude(x => x.UserRoleType)
                .FirstOrDefaultAsync(x => x.LDAPName == _currentUserService.UserId, cancellationToken);
            if (user == null)
            {
                return false;
            }
            var e = await _context.Events.FindAsync(request.EventId);
            if (e.OwnerId == user.Id || user.Roles.Any(x => x.UserRoleType.Name == "Administrator"))
            {
                return true;
            }
            return false;
        }
    }
}
