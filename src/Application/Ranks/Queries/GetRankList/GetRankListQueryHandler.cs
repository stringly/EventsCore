using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventsCore.Application.Common.Interfaces;
using EventsCore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.Ranks.Queries.GetRankList
{
    /// <summary>
    /// Implementation of <see cref="IRequestHandler{TRequest, TResponse}"></see> that handles requests for a list of <see cref="Rank"></see>
    /// </summary>
    public class GetRankListQueryHandler : IRequestHandler<GetRankListQuery, RankListVm>
    {
        private readonly IEventsCoreDbContext _context;
        private readonly IMapper _mapper;
        /// <summary>
        /// Creates a new instance of the Handler
        /// </summary>
        /// <param name="context">An implementation of <see cref="IEventsCoreDbContext"></see></param>
        /// <param name="mapper">An implementation of <see cref="IMapper"></see></param>
        public GetRankListQueryHandler(IEventsCoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Handles the Request
        /// </summary>
        /// <param name="request">A <see cref="GetRankListQuery"></see> request object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"></see></param>
        /// <returns>A <see cref="RankListVm"></see> containing the list of <see cref="Rank"></see> projected to <see cref="RankDto"></see></returns>
        public async Task<RankListVm> Handle(GetRankListQuery request, CancellationToken cancellationToken)
        {
            var ranks = await _context.Ranks
                .ProjectTo<RankDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            var vm = new RankListVm
            {
                Ranks = ranks,
                Count = ranks.Count
            };
            return vm;
        }
    }
}
