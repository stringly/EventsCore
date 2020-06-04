using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.EventSerieses.Commands.UpsertEventSeries;
using EventsCore.Application.UnitTests.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.Application.UnitTests.EventSerieses.Commands
{
    public class UpsertEventSeriesesCommandTests : CommandTestBase
    {
        private readonly UpsertEventSeriesesCommandHandler _sut;

        public UpsertEventSeriesesCommandTests() : base()
        {
            _sut = new UpsertEventSeriesesCommandHandler(_context);
        }
        [Fact]
        public async Task Handle_Given_InvalidId_Throws_NotFoundException()
        {
            // Arrange
            var invalidId = 0;
            var command = new UpsertEventSeriesesCommand { Id = invalidId };
            
            // Act/Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
        }
        [Fact]
        public void Handle_Given_No_Id_And_Valid_Values_Creates_EventSeries()
        {
            // Arrange
            var validTitle = "Testing Event Series Title";
            var validDescription = "This is the description of an event series created in the testing module.";
            var command = new UpsertEventSeriesesCommand { Title = validTitle, Description = validDescription };

            // Act
            var result = _sut.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(_context.EventSeries.Any(x => x.Title == validTitle));
        }
        [Fact]
        public async Task Handle_Given_Valid_Id_And_Valid_Values_Updates_EventSeries()
        {
            // Arrange
            var validId = 1;
            var validNewTitle = "Updated Event Series Title";
            var validNewDescription = "This is an updated Event Series Description";
            var command = new UpsertEventSeriesesCommand { Id = validId, Title = validNewTitle, Description = validNewDescription};

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            var eventType = await _context.EventSeries.FindAsync(1);
            Assert.Equal(validNewTitle, eventType.Title);
            Assert.Equal(validNewDescription, eventType.Description);
        }
        [Fact]
        public async Task Handle_Given_No_Id_And_Taken_Title_Throws_ValidationException()
        {
            // Arrange
            var takenTitle = "Event Series 1";
            var validDescription = "A valid Event Series Description";
            var command = new UpsertEventSeriesesCommand { Title = takenTitle, Description = validDescription};

            // Act/Assert
            await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));
            
        }
        [Fact]
        public async Task Handle_Given_Valid_Id_And_Taken_Title_Throws_ValidationException()
        {
            // Arrange
            var validId = 2; // Id = 2 should be "Event Series 2", so attempting to update the Title using the title of series 1 should throw the exception.
            var takenTitle = "Event Series 1";
            var validDescription = "A valid Event Series Description";
            var command = new UpsertEventSeriesesCommand { Id = validId, Title = takenTitle, Description = validDescription };

            // Act/Assert
            await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));
        }
        [Fact]
        public async Task Handle_Given_Title_TooLong_Is_Invalid()
        {
            // Arrange
            var tooLongTitle = "AAAAABBBBBCCCCCDDDDDEEEEEFFFFF";
            var validDescription = "A valid Event Series Description";
            var command = new UpsertEventSeriesesCommand { Title = tooLongTitle, Description = validDescription };
            var validator = new UpsertEventSeriesesCommandValidator();

            // Act/Assert
            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
        }
    }
}
