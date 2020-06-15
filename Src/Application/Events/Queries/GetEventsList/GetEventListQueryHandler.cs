using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsCore.Application.Common.Interfaces;
using EventsCore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.Events.Queries.GetEventsList
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"></see> that handles requests for a list of <see cref="EventSeries"></see>
    /// </summary>
    public class GetEventListQueryHandler : IRequestHandler<GetEventListQuery, EventListVm>
    {
        private IEventsCoreDbContext _context;
        private readonly IMapper _mapper;
        /// <summary>
        /// Creates a new instance of the Handler
        /// </summary>
        /// <param name="context">An implementation of <see cref="IEventsCoreDbContext"></see></param>
        /// <param name="mapper">An implementation of <see cref="IMapper"></see></param>
        public GetEventListQueryHandler(IEventsCoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Handles the Request
        /// </summary>
        /// <param name="request">A <see cref="GetEventListQuery"></see> request object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"></see></param>
        /// <returns>A <see cref="EventListVm"></see> containing the list of <see cref="Event"></see> projected to <see cref="EventDto"></see></returns>
        public async Task<EventListVm> Handle(GetEventListQuery request, CancellationToken cancellationToken)
        {
            var events = await _context.Events
                .Skip((request.CurrentPage - 1) * request.PageSize)
                .Take(request.PageSize)
                .Where(x => (string.IsNullOrEmpty(request.SearchString) || x.Title.Contains(request.SearchString)) && (request.EventTypeId == null || x.EventTypeId == request.EventTypeId))
                .ProjectTo<EventDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            var vm = new EventListVm
            {
                Events = events,
                Count = events.Count
            };
            return vm;
        }


    }
}
