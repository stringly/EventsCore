using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.EventSerieses.Commands.DeleteEventSeries;
using EventsCore.Application.UnitTests.Common;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.Application.UnitTests.EventSerieses.Commands
{
    public class DeleteEventSeriesesCommandTests : CommandTestBase
    {
        private readonly DeleteEventSeriesesCommandHandler _sut;

        public DeleteEventSeriesesCommandTests() : base()
        {
            _sut = new DeleteEventSeriesesCommandHandler(_context);
        }
        [Fact]
        public async Task Handle_Given_InvalidId_Throws_NotFoundException()
        {
            // Arrange
            var invalidId = 0;
            var command = new DeleteEventSeriesesCommand { Id = invalidId };

            // Act/Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
        }
        [Fact]
        public async Task Handle_Given_Valid_Id_And_Zero_Events_Deletes_EventSeries()
        {
            // Arrange
            var validId = 2; // series 2 should not have events
            var command = new DeleteEventSeriesesCommand { Id = validId };

            // Act
            await _sut.Handle(command, CancellationToken.None);
            
            // Assert
            var eventSeries = await _context.EventSeries.FindAsync(validId);
            Assert.Null(eventSeries);
        }
        [Fact]
        public async Task Handle_Given_Valid_Id_And_Some_Events_Throws_DeleteFailureException()
        {
            // Arrange
            var validId = 1;
            var command = new DeleteEventSeriesesCommand { Id = validId };
            
            // Act/Assert
            await Assert.ThrowsAsync<DeleteFailureException>(() => _sut.Handle(command, CancellationToken.None));
        }
    }
}
