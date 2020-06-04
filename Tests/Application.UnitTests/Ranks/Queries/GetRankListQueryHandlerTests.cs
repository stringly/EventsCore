using AutoMapper;
using EventsCore.Application.Ranks.Queries.GetRankList;
using EventsCore.Application.UnitTests.Common;
using EventsCore.Persistence;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.Application.UnitTests.Ranks.Queries
{
    [Collection("QueryCollection")]
    public class GetRankListQueryHandlerTests
    {
        private readonly EventsCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetRankListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }
        [Fact]
        public async Task Get_Ranks_Test()
        {
            // Arrange
            var sut = new GetRankListQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetRankListQuery(), CancellationToken.None);

            // Assert
            result.ShouldBeOfType<RankListVm>();
            result.Ranks.Count.ShouldBe(3);
        }
    }
}
