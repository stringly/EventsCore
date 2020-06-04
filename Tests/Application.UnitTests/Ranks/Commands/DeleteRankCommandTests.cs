using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Ranks.Commands.DeleteRank;
using EventsCore.Application.UnitTests.Common;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.Application.UnitTests.Ranks.Commands
{
    public class DeleteRankCommandTests : CommandTestBase
    {
        private readonly DeleteRankCommandHandler _sut;

        public DeleteRankCommandTests() : base()
        {
            _sut = new DeleteRankCommandHandler(_context);
        }
        [Fact]
        public async Task Handle_Given_InvalidId_Throws_NotFoundException()
        {
            // Arrange
            var invalidId = 0;
            var command = new DeleteRankCommand { Id = invalidId };

            // Act/Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
        }
        [Fact]
        public async Task Handle_Given_Valid_Id_And_Zero_Users_Deletes_Rank()
        {
            // Arrange
            var validId = 3; // Rank Id = 3 should not have any users
            var command = new DeleteRankCommand { Id = validId };

            // Act
            await _sut.Handle(command, CancellationToken.None);
            var rank = await _context.Ranks.FindAsync(validId);

            // Assert
            Assert.Null(rank);    
        }
        [Fact]
        public async Task Handle_Given_Valid_Id_And_Some_Users_Throws_DeleteFailureException()
        {
            // Arrange
            var validId = 1; // Rank Id = 1 should have 1 user
            var command = new DeleteRankCommand { Id = validId };

            // Act/Assert
            await Assert.ThrowsAsync<DeleteFailureException>(() => _sut.Handle(command, CancellationToken.None));
        }
    }
}
