using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Registrations.Commands.CreateRegistration;
using EventsCore.Application.UnitTests.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.Application.UnitTests.Registrations.Commands
{
    public class CreateRegistrationsTests : CommandTestBase
    {
        private readonly CreateRegistrationCommandHandler _sut;

        public CreateRegistrationsTests() : base()
        {
            _sut = new CreateRegistrationCommandHandler(_context, new DateTimeTestProvider());
        }
        [Fact]
        public async Task Handle_Given_Valid_Values_Creates_Registration()
        {
            // Arrange
            var validEventId = 3;
            var validUserId = 1;
            var command = new CreateRegistrationCommand
            {
                EventId = validEventId,
                UserId = validUserId
            };

            // Act
            await _sut.Handle(command, CancellationToken.None);
            var entity = _context.Events.Find(validEventId);
            // Assert
            Assert.Contains(entity.Registrations, x => x.UserId == validUserId);
            Assert.Single(entity.Registrations);
        }
        [Fact]
        public async Task Handle_Given_Invalid_UserId_Throws_NotFoundException()
        {
            // Arrange
            var inValidUserId = 0;
            var validEventId = 3;
            var command = new CreateRegistrationCommand
            {
                EventId = validEventId,
                UserId = inValidUserId
            };
            // Act/Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>  _sut.Handle(command, CancellationToken.None));            
        }
        [Fact]
        public async Task Handle_Given_Invalid_EventId_Throws_NotFoundException()
        {
            // Arrange
            var validUserId = 1;
            var inValidEventId = 0;
            var command = new CreateRegistrationCommand
            {
                EventId = inValidEventId,
                UserId = validUserId
            };
            // Act/Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
        }

    }
}
