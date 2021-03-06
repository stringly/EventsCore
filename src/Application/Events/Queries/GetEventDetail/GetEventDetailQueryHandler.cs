﻿using AutoMapper;
using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Common.Interfaces;
using EventsCore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.Events.Queries.GetEventDetail
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"></see> that handles requests for a list of <see cref="Event"></see>
    /// </summary>
    public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, EventDetailDto>
    {
        private readonly IEventsCoreDbContext _context;
        private readonly IMapper _mapper;
        /// <summary>
        /// Creates a new instance of the Handler
        /// </summary>
        /// <param name="context">An implementation of <see cref="IEventsCoreDbContext"></see></param>
        /// <param name="mapper">An implementation of <see cref="IMapper"></see></param>
        public GetEventDetailQueryHandler(IEventsCoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Handles the Request
        /// </summary>
        /// <param name="request">A <see cref="GetEventDetailQuery"></see> request object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"></see></param>
        /// <returns>A <see cref="EventDetailDto"></see> containing the details for a <see cref="Event"></see></returns>
        public async Task<EventDetailDto> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Events
                
                .Include(x => x.EventType)
                .Include(x => x.EventSeries)
                .Include(x => x.Owner)
                    .ThenInclude(x => x.Rank)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Event), request.Id);
            }
            var vm = _mapper.Map<EventDetailDto>(entity);
            return vm;
                
                
        }
    }
}
