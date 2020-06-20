using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Registrations.Commands.DeleteRegistration;
using EventsCore.Application.UnitTests.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.Application.UnitTests.Registrations.Commands
{
    public class DeleteRegistrationCommandTests : CommandTestBase
    {
        private readonly DeleteRegistrationCommandHandler _sut;

        public DeleteRegistrationCommandTests() : base()
        {
            _sut = new DeleteRegistrationCommandHandler(_context);
        }
        [Fact]
        public async Task Handle_Given_Valid_Values_Can_Delete_Registration()
        {
            // Arrange
            var validRegId = 1;
            var validEventId = 3;
            var command = new DeleteRegistrationCommand
            {
                EventId = validEventId,
                RegistrationId = validRegId
            };

            // Act
            await _sut.Handle(command, CancellationToken.None);

            // Assert
            var entity = _context.Events.Find(validEventId);
            Assert.Empty(entity.Registrations);
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]

        public async Task Handle_Given_Invalid_EventId_Throws_NotFoundException(int value)
        {
            // Arrange
            var validRegId = 1;
            var inValidEventId = value;
            var command = new DeleteRegistrationCommand
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
            var command = new DeleteRegistrationCommand
            {
                EventId = validEventId,
                RegistrationId = inValidRegId
            };

            // Act/Assert
            await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));

        }
    }
}
