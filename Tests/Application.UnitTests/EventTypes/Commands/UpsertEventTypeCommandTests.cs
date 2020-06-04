using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.EventTypes.Commands.UpsertEventType;
using EventsCore.Application.UnitTests.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.Application.UnitTests.EventTypes.Commands
{
    public class UpsertEventTypeCommandTests : CommandTestBase
    {
        private readonly UpsertEventTypeCommandHandler _sut;

        public UpsertEventTypeCommandTests() : base()
        {
            _sut = new UpsertEventTypeCommandHandler(_context);
        }
        [Fact]
        public async Task Handle_Given_InvalidId_Throws_NotFoundException()
        {
            // Arrange
            var invalidId = 0;
            var command = new UpsertEventTypeCommand { Id = invalidId };

            // Act/Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
        }
        [Fact]
        public void Handle_Given_No_Id_And_Valid_Name_Creates_EventType()
        {
            // Arrange
            var name = "Event Type #3";
            var command = new UpsertEventTypeCommand { Name = name };

            // Act
            var result = _sut.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(_context.EventTypes.Any(x => x.Name == name));
        }
        [Fact]
        public async Task Handle_Given_Valid_Id_And_Valid_Name_Updates_EventType()
        {
            // Arrange
            var validId = 1;
            var newName = "Updated Event Type Name";
            var command = new UpsertEventTypeCommand { Id = validId, Name = newName };

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(_context.EventTypes.Find(1).Name, newName);
        }
        [Fact]
        public async Task Handle_Given_No_Id_And_Taken_Name_Throws_ValidationException()
        {
            // Arrange
            var takenName = "Training";
            var command = new UpsertEventTypeCommand { Name = takenName };

            // Act/Assert
            await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));    
        }
        [Fact]
        public async Task Handle_Given_Valid_Id_And_Taken_Name_Throws_ValidationException()
        {
            // Arrange
            var validId = 1; // Id = 1 should be the "Training" Type
            var takenName = "Overtime"; // "Overtime" should be the name of the Id = 2 EventType
            var command = new UpsertEventTypeCommand { Id = validId, Name = takenName };

            // Act/Assert
            await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));
        }
        [Fact]
        public async Task Handle_Given_Name_TooLong_Is_Invalid()
        {
            // Arrange
            var tooLongName = "AAAAABBBBBCCCCCDDDDDEEEEEFFFFF";
            var command = new UpsertEventTypeCommand { Name = tooLongName };
            var validator = new UpsertEventTypeCommandValidator();

            // Act/Assert
            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
        }
    }
}
