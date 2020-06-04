using AutoMapper;
using EventsCore.Application.EventSerieses.Queries.GetEventSeriesesList;
using EventsCore.Application.UnitTests.Common;
using EventsCore.Persistence;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.Application.UnitTests.EventSerieses.Queries
{
    [Collection("QueryCollection")]
    public class GetEventSeriesesListQueryHandlerTests
    {
        private readonly EventsCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetEventSeriesesListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }
        [Fact]
        public async Task Get_EventSeries_Test()
        {
            // Arrange
            var sut = new GetEventSeriesesListQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetEventSeriesesListQuery(), CancellationToken.None);

            // Assert
            result.ShouldBeOfType<EventSeriesesListVm>();
            result.EventSerieses.Count.ShouldBe(2);
        }
    }
}
