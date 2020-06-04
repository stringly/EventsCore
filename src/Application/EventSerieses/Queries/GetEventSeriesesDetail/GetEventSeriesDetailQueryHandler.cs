using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Common.Interfaces;
using EventsCore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.EventSerieses.Queries.GetEventSeriesesDetail
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"></see> that handles requests for a list of <see cref="EventSeries"></see>
    /// </summary>
    public class GetEventSeriesDetailQueryHandler : IRequestHandler<GetEventSeriesDetailQuery, EventSeriesDetailVm>
    {
        private readonly IEventsCoreDbContext _context;
        private readonly IMapper _mapper;
        /// <summary>
        /// Creates a new instance of the Handler
        /// </summary>
        /// <param name="context">An implementation of <see cref="IEventsCoreDbContext"></see></param>
        /// <param name="mapper">An implementation of <see cref="IMapper"></see></param>
        public GetEventSeriesDetailQueryHandler(IEventsCoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Handles the Request
        /// </summary>
        /// <param name="request">A <see cref="GetEventSeriesDetailQuery"></see> request object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"></see></param>
        /// <returns>A <see cref="EventSeriesDetailVm"></see> containing the details for a <see cref="EventSeries"></see></returns>
        public async Task<EventSeriesDetailVm> Handle(GetEventSeriesDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.EventSeries
                .FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(EventSeries), request.Id);
            }
            var vm = _mapper.Map<EventSeriesDetailVm>(entity);
            vm.Events = await _context.Events
                .Where(x => x.EventSeriesId == request.Id)
                .ProjectTo<EventSeriesEventDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return vm;
        }
    }
}
