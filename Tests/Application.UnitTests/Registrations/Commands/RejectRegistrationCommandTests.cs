using EventsCore.Application.Registrations.Commands.RejectRegistration;
using EventsCore.Application.UnitTests.Common;
using EventsCore.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.Application.UnitTests.Registrations.Commands
{
    public class RejectRegistrationCommandTests : CommandTestBase
    {
        private readonly RejectRegistrationCommandHandler _sut;

        public RejectRegistrationCommandTests() : base()
        {
            _sut = new RejectRegistrationCommandHandler(_context, new DateTimeTestProvider());
        }
        [Fact]
        public async Task Handle_Given_Valid_Values_Rejects_Registration()
        {
            // Arrange
            var validRegId = 1;
            var validEventId = 3;
            var command = new RejectRegistrationCommand
            {
                EventId = validEventId,
                RegistrationId = validRegId
            };

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            var entity = _context.Events.Find(validEventId);
            var reg = entity.Registrations.FirstOrDefault(x => x.Id == validRegId);
            Assert.Equal(RegistrationStatus.Rejected, reg.Status);
        }
    }
}
