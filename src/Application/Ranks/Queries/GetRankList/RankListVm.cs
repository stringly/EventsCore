using EventsCore.Domain.Entities;
using System.Collections.Generic;

namespace EventsCore.Application.Ranks.Queries.GetRankList
{
    /// <summary>
    /// Class that serves as a Viewmodel for a list of <see cref="Rank"></see> entities
    /// </summary>
    public class RankListVm
    {
        /// <summary>
        /// A list of <see cref="RankDto"></see> objects
        /// </summary>
        public IList<RankDto> Ranks { get; set; }
        /// <summary>
        /// The count of entites in the list
        /// </summary>
        public int Count { get; set; }
    }
}
