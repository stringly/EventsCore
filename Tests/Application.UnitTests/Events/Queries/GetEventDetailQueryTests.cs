using AutoMapper;
using EventsCore.Application.Events.Queries.GetEventDetail;
using EventsCore.Application.UnitTests.Common;
using EventsCore.Persistence;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.Application.UnitTests.Events.Queries
{
    [Collection("QueryCollection")]
    public class GetEventDetailQueryTests
    {
        private readonly EventsCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetEventDetailQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }
        [Fact]
        public async Task Handle_Get_EventDetail_Returns_Event()
        {
            // Arrange
            var sut = new GetEventDetailQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetEventDetailQuery { Id = 1 }, CancellationToken.None);

            // Assert

            result.ShouldBeOfType<EventDetailDto>();
            result.Id.ShouldBe(1);
            Assert.Equal("Training", result.EventTypeTitle);
        }
    }
}
