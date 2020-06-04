using AutoMapper;
using EventsCore.Application.EventSerieses.Queries.GetEventSeriesesDetail;
using EventsCore.Application.UnitTests.Common;
using EventsCore.Persistence;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.Application.UnitTests.EventSerieses.Queries
{
    [Collection("QueryCollection")]
    public class GetEventSeriesDetailQueryHandlerTests
    {
        private readonly EventsCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetEventSeriesDetailQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Get_EventSeries_Detail()
        {
            // Arrange
            var sut = new GetEventSeriesDetailQueryHandler(_context, _mapper);
            
            // Act
            var result = await sut.Handle(new GetEventSeriesDetailQuery { Id = 1}, CancellationToken.None);

            // Assert

            result.ShouldBeOfType<EventSeriesDetailVm>();
            result.Id.ShouldBe(1);
            result.Events.Count.ShouldBe(1);
        }

    }
}
