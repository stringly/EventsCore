using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsCore.Application.Common.Interfaces;
using EventsCore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.EventSerieses.Queries.GetEventSeriesesList
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"></see> that handles requests for a list of <see cref="EventSeries"></see>
    /// </summary>
    public class GetEventSeriesesListQueryHandler : IRequestHandler<GetEventSeriesesListQuery, EventSeriesesListVm>
    {
        private readonly IEventsCoreDbContext _context;
        private readonly IMapper _mapper;
        /// <summary>
        /// Creates a new instance of the Handler
        /// </summary>
        /// <param name="context">An implementation of <see cref="IEventsCoreDbContext"></see></param>
        /// <param name="mapper">An implementation of <see cref="IMapper"></see></param>
        public GetEventSeriesesListQueryHandler(IEventsCoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Handles the Request
        /// </summary>
        /// <param name="request">A <see cref="GetEventSeriesesListQuery"></see> request object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"></see></param>
        /// <returns>A <see cref="EventSeriesesListVm"></see> containing the list of <see cref="EventSeries"></see> projected to <see cref="EventSeriesDto"></see></returns>
        public async Task<EventSeriesesListVm> Handle(GetEventSeriesesListQuery request, CancellationToken cancellationToken)
        {
            var eventSeries = await _context.EventSeries
                .ProjectTo<EventSeriesDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            var vm = new EventSeriesesListVm
            {
                EventSerieses = eventSeries,
                Count = eventSeries.Count
            };
            return vm;
        }
    }
}
