using AutoMapper;
using AutoMapper.Configuration.Conventions;
using AutoMapper.QueryableExtensions;
using EventsCore.Application.Common.Interfaces;
using EventsCore.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.Events.Queries.GetUpcomingEvents
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"></see> that handles requests for a list of <see cref="UpcomingEventDto"></see>
    /// </summary>
    public class GetUpcomingEventsQueryHandler : IRequestHandler<GetUpcomingEventsQuery, UpcomingEventListVm>
    {
        private readonly IEventsCoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;
        /// <summary>
        /// Creates a new instance of the Handler
        /// </summary>
        /// <param name="context">An implementation of <see cref="IEventsCoreDbContext"></see></param>
        /// <param name="mapper">An implementation of <see cref="IMapper"></see></param>
        /// <param name="dateTime">An implementation of <see cref="IDateTime"/></param>
        /// <param name="currentUserService">An implementation of <see cref="ICurrentUserService"/></param>
        public GetUpcomingEventsQueryHandler(IEventsCoreDbContext context, IMapper mapper, IDateTime dateTime, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _dateTime = dateTime;
            _currentUserService = currentUserService;
        }
        /// <summary>
        /// Handles the Request
        /// </summary>
        /// <param name="request">A <see cref="GetUpcomingEventsQuery"></see> request object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"></see></param>
        /// <returns>A <see cref="UpcomingEventListVm"></see> containing the list of <see cref="Domain.Entities.Event"></see> projected to <see cref="UpcomingEventDto"></see></returns>
        public async Task<UpcomingEventListVm> Handle(GetUpcomingEventsQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.LDAPName == _currentUserService.UserId);
            int currentUserId = 0;
            if (currentUser != null)
            {
                currentUserId = currentUser.Id;
            }
            DateTime startOfRange = _dateTime.Now;
            DateTime endOfRange = startOfRange.AddDays(60);
            var events = await _context.Events
                .Include(x => x.EventSeries)
                .Include(x => x.EventType)
                .Include(x => x.Owner)
                    .ThenInclude(x => x.Rank)
                .Include(x => x.Registrations)
                .Where(x => (x.Dates.RegistrationStartDate > startOfRange && x.Dates.RegistrationEndDate < endOfRange)
                    && x.Rules.MaxRegistrations > x.Registrations.Count(x => x.Status == Domain.Entities.RegistrationStatus.Accepted)
                    && x.Registrations.Any(x => x.UserId == currentUserId) == false
                )
                .ProjectTo<UpcomingEventDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
                
            
            var vm = new UpcomingEventListVm
            {
                Events = events,
                Count = events.Count
            };
            return vm;
        }
    }
}
