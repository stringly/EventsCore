using AutoMapper;
using EventsCore.Application.EventTypes.Queries.GetEventTypesList;
using EventsCore.Application.UnitTests.Common;
using EventsCore.Persistence;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.Application.UnitTests.EventTypes.Queries
{
    [Collection("QueryCollection")]
    public class GetEventTypeListQueryHandlerTests
    {
        private readonly EventsCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetEventTypeListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }
        [Fact]
        public async Task Get_EventTypes_Test()
        {
            // Arrange
            var sut = new GetEventTypeListQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetEventTypeListQuery(), CancellationToken.None);

            // Assert
            result.ShouldBeOfType<EventTypeListVm>();
            result.EventTypes.Count.ShouldBe(3);

        }
    }
}
