using AutoMapper;
using EventsCore.Application.Events.Queries.GetUpcomingEvents;
using EventsCore.Application.UnitTests.Common;
using EventsCore.Common;
using EventsCore.Persistence;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.Application.UnitTests.Events.Queries
{
    [Collection("QueryCollection")]
    public class GetUpcomingEventsQueryTests
    {
        private readonly EventsCoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;

        public GetUpcomingEventsQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
            _dateTime = new DateTimeTestProvider();
        }
        [Fact]
        public async Task Handle_Get_Upcoming_Events_Returns_Events()
        {
            // Arrange
            var currentUserMock = new CurrentUserServiceTesting("user456", true); // user456 should be Id = 2;
            var sut = new GetUpcomingEventsQueryHandler(_context, _mapper, _dateTime, currentUserMock);

            // Act
            var result = await sut.Handle(new GetUpcomingEventsQuery(), CancellationToken.None);

            // Assert
            result.ShouldBeOfType<UpcomingEventListVm>();
            result.Events.Count.ShouldBe(1);

        }
    }
}
