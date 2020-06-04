using MediatR;

namespace EventsCore.Application.Ranks.Queries.GetRankList
{
    /// <summary>
    /// Implementation of <see cref="IRequest"></see> that returns a list of <see cref="RankDto"></see> in an <see cref="RankListVm"/>
    /// </summary>
    public class GetRankListQuery : IRequest<RankListVm>
    {
    }
}
