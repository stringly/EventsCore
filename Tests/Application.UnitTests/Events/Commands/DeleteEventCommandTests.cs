using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Events.Commands.DeleteEvent;
using EventsCore.Application.UnitTests.Common;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.Application.UnitTests.Events.Commands
{
    public class DeleteEventCommandTests : CommandTestBase
    {
        private readonly DeleteEventCommandHandler _sut;
        public DeleteEventCommandTests() : base()
        {
            _sut = new DeleteEventCommandHandler(_context);
        }
        [Fact]
        public async Task Handle_Given_InvalidId_Throws_NotFoundException()
        {
            // Arrange
            var invalidId = 0;
            var command = new DeleteEventCommand { Id = invalidId };

            // Act/Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
        }
        [Fact]
        public async Task Handle_Given_ValidId_Deletes_Event()
        {
            // Arrange
            var validId = 1;
            var command = new DeleteEventCommand { Id = validId };

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(_context.Events.Find(validId));
        }
    }
}
