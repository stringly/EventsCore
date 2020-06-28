using EventsCore.Application.Events.Queries.GetCanEditEvent;
using EventsCore.Application.UnitTests.Common;
using EventsCore.Persistence;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.Application.UnitTests.Events.Queries
{
    [Collection("QueryCollection")]
    public class GetCanEditEventTests
    {
        private readonly EventsCoreDbContext _context;

        public GetCanEditEventTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }
        [Fact]
        public async Task Handle_Get_EventEdit_With_Admin_User_Returns_True()
        {
            // Arrange
            var currentUserMock = new CurrentUserServiceTesting("jcs30", true);
            var sut = new GetCanEditEventQueryHandler(_context, currentUserMock);

            // Act
            var result = await sut.Handle(new GetCanEditEventQuery { EventId = 1 }, CancellationToken.None);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public async Task Handle_Get_EventEdit_With_Unknown_User_Returns_False()
        {
            // Arrange
            var currentUserMock = new CurrentUserServiceTesting("user456", true);
            var sut = new GetCanEditEventQueryHandler(_context, currentUserMock);

            // Act
            var result = await sut.Handle(new GetCanEditEventQuery { EventId = 1}, CancellationToken.None);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public async Task Handle_Get_EventEdit_With_Non_Admin_Non_Owner_User_Returns_False()
        {
            // Arrange
            var currentUserMock = new CurrentUserServiceTesting("user234", true);
            var sut = new GetCanEditEventQueryHandler(_context, currentUserMock);

            // Act
            var result = await sut.Handle(new GetCanEditEventQuery { EventId = 1}, CancellationToken.None);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public async Task Handle_Get_EventEdit_With_Event_Owner_Returns_True()
        {
            // Arrange
            var currentUserMock = new CurrentUserServiceTesting("user123", true);
            var sut = new GetCanEditEventQueryHandler(_context, currentUserMock);

            // Act
            var result = await sut.Handle(new GetCanEditEventQuery { EventId = 1 }, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

    }
}
