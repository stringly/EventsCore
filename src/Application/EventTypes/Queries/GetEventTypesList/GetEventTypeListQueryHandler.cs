using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsCore.Application.Common.Interfaces;
using EventsCore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.EventTypes.Queries.GetEventTypesList
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"></see> that handles requests for a list of <see cref="EventType"></see>
    /// </summary>
    public class GetEventTypeListQueryHandler : IRequestHandler<GetEventTypeListQuery, EventTypeListVm>
    {
        private readonly IEventsCoreDbContext _context;
        private readonly IMapper _mapper;
        /// <summary>
        /// Creates a new instance of the Handler
        /// </summary>
        /// <param name="context">An implementation of <see cref="IEventsCoreDbContext"></see></param>
        /// <param name="mapper">An implementation of <see cref="IMapper"></see></param>
        public GetEventTypeListQueryHandler(IEventsCoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Handles the Request
        /// </summary>
        /// <param name="request">A <see cref="GetEventTypeListQuery"></see> request object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"></see></param>
        /// <returns>A <see cref="EventTypeListVm"></see> containing the list of <see cref="EventTypes"></see> projected to <see cref="EventTypeDto"></see></returns>
        public async Task<EventTypeListVm> Handle(GetEventTypeListQuery request, CancellationToken cancellationToken)
        {
            var eventTypes = await _context.EventTypes
                .ProjectTo<EventTypeDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            var vm = new EventTypeListVm
            {
                EventTypes = eventTypes,
                Count = eventTypes.Count
            };
            return vm;
        }

    }
}
