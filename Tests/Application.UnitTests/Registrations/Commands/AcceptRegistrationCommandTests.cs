using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Registrations.Commands.AcceptRegistration;
using EventsCore.Application.UnitTests.Common;
using EventsCore.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.Application.UnitTests.Registrations.Commands
{
    public class AcceptRegistrationCommandTests : CommandTestBase
    {
        private readonly AcceptRegistrationCommandHandler _sut;

        public AcceptRegistrationCommandTests() : base()
        {
            _sut = new AcceptRegistrationCommandHandler(_context, new DateTimeTestProvider());
        }
        [Fact]
        public async Task Handle_Given_Valid_Values_Accepts_Registration()
        {
            // Arrange
            var validRegId = 1;
            var validEventId = 3;
            var command = new AcceptRegistrationCommand
            {
                EventId = validEventId,
                RegistrationId = validRegId
            };
            
            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            var entity = _context.Events.Find(validEventId);
            var reg = entity.Registrations.FirstOrDefault(x => x.Id == validRegId);
            Assert.Equal(RegistrationStatus.Accepted, reg.Status);
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]

        public async Task Handle_Given_Invalid_EventId_Throws_NotFoundException(int value)
        {
            // Arrange
            var validRegId = 1;
            var inValidEventId = value;
            var command = new AcceptRegistrationCommand
            {
                EventId = inValidEventId,
                RegistrationId = validRegId
            };
            // Act/Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
        }
        [Fact]
        public async Task Handle_Given_Invalid_RegistrationId_Throws_ValidationException()
        {
            // Arrange
            var inValidRegId = 0;
            var validEventId = 3;
            var command = new AcceptRegistrationCommand
            {
                EventId = validEventId,
                RegistrationId = inValidRegId
            };

            // Act/Assert
            await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));

        }
    }
}
