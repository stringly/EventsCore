using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.EventTypes.Commands.DeleteEventType;
using EventsCore.Application.UnitTests.Common;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.Application.UnitTests.EventTypes.Commands
{
    public class DeleteEventTypeCommandTests : CommandTestBase
    {
        private readonly DeleteEventTypeCommandHandler _sut;

        public DeleteEventTypeCommandTests() : base()
        {
            _sut = new DeleteEventTypeCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_Given_InvalidId_Throws_NotFoundException()
        {
            // Arrange
            var invalidId = 0;
            var command = new DeleteEventTypeCommand { Id = invalidId };

            // Act/Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
        }
        [Fact]
        public async Task Handle_Given_Valid_Id_And_Zero_Events_Deletes_EventType()
        {
            // Arrange

            var validId = 2;
            var command = new DeleteEventTypeCommand { Id = validId };

            // Act
            await _sut.Handle(command, CancellationToken.None);

            var eventType = await _context.EventTypes.FindAsync(validId);

            // Assert
            Assert.Null(eventType);
        }
        [Fact]
        public async Task Handle_Given_Valid_Id_And_Some_Events_Throws_DeleteFailureException()
        {
            // Arrange
            var validId = 1; // EventType 1 should have 1 Event in the test context
            var command = new DeleteEventTypeCommand { Id = validId };
            // Act/Assert
            await Assert.ThrowsAsync<DeleteFailureException>(() => _sut.Handle(command, CancellationToken.None));
        }


    }
}
