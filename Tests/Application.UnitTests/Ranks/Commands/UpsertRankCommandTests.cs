using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Ranks.Commands.UpsertRank;
using EventsCore.Application.UnitTests.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.Application.UnitTests.Ranks.Commands
{
    public class UpsertRankCommandTests : CommandTestBase
    {
        private readonly UpsertRankCommandHandler _sut;
        public UpsertRankCommandTests() : base()
        {
            _sut = new UpsertRankCommandHandler(_context);
        }
        [Fact]
        public async Task Handle_Given_InvalidId_Throws_NotFoundException()
        {
            // Arrange
            var invalidId = 0;
            var command = new UpsertRankCommand { Id = invalidId };

            // Act/Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
        }
        [Fact]
        public async Task Handle_Given_No_Id_And_Valid_Values_Creates_Rank()
        {
            // Arrange
            var validAbbrev = "TEST";
            var validFullName = "TEST RANK";
            var command = new UpsertRankCommand { Abbrev = validAbbrev, FullName = validFullName };

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(_context.Ranks.Any(x => x.Abbreviation == validAbbrev));
        }
        [Fact]
        public async Task Handle_Given_Valid_Id_And_Valid_Values_Updates_Rank()
        {
            // Arrange
            var validId = 1;
            var validAbbrev = "P/O";
            var validFullName = "Police Officer";
            var command = new UpsertRankCommand { Id = validId, Abbrev = validAbbrev, FullName = validFullName };

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            var updated = await _context.Ranks.FindAsync(validId);
            Assert.Equal(validAbbrev, updated.Abbreviation);
            Assert.Equal(validFullName, updated.FullName);            
        }
        [Fact]
        public async Task Handle_Given_No_Id_And_Taken_Abbrev_Throws_ValidationException()
        {
            // Arrange
            var takenAbbrev = "Pvt.";
            var validFullName = "Police Officer";
            var command = new UpsertRankCommand { Abbrev = takenAbbrev, FullName = validFullName };

            // Act/Assert
            await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));            
        }
        [Fact]
        public async Task Handle_Given_No_Id_And_Taken_FullName_Throws_ValidationException()
        {
            // Arrange
            var validAbbrev = "P/O";
            var takenFullName = "Private";
            var command = new UpsertRankCommand { Abbrev = validAbbrev, FullName = takenFullName };

            // Act/Assert
            await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));
        }
        [Fact]
        public async Task Handle_Given_ValidId_And_Taken_Abbrev_Throws_ValidationException()
        {
            // Arrange
            var validId = 1; // 1 Should be the "P/O"
            var takenAbbrev = "POFC"; // should be the abbrev for rank id = 2
            var validFullName = "Police Officer";
            var command = new UpsertRankCommand { Id = validId, Abbrev = takenAbbrev, FullName = validFullName };

            // Act/Assert
            await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));
        }
        [Fact]
        public async Task Handle_Given_ValidId_And_Taken_FullName_Throws_ValidationException()
        {
            // Arrange
            var validId = 1;
            var validAbbrev = "P/O";
            var takenFullName = "Police Officer First Class";
            var command = new UpsertRankCommand { Id = validId, Abbrev = validAbbrev, FullName = takenFullName };

            // Act/Assert
            await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));
        }
    }
}
