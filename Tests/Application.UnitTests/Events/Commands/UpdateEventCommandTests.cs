using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Events.Commands.UpdateEvent;
using EventsCore.Application.UnitTests.Common;
using EventsCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.Application.UnitTests.Events.Commands
{
    public class UpdateEventCommandTests : CommandTestBase
    {
        private readonly UpdateEventCommandHandler _sut;
        public UpdateEventCommandTests() : base()
        {
            _sut = new UpdateEventCommandHandler(_context, new DateTimeTestProvider());
        }
        [Fact]
        public async Task Handle_Given_Minimum_Valid_Values_Updates_Event()
        {
            // Arrange
            var validId = 1;
            var newValidTitle = "Updated Event Title";
            var newValidDescription = "This is an updated Event description";
            var newValidStartDate = new DateTime(2020, 10, 1);
            var newValidEndDate = new DateTime(2020, 10, 2);
            var newValidRegStartDate = new DateTime(2020, 9, 1);
            var newValidRegEndDate = new DateTime(2020, 9, 2);
            var newValidMaxRegs = 20;
            var newValidEventTypeId = 2;
            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = newValidTitle,
                Description = newValidDescription,
                StartDate = newValidStartDate,
                EndDate = newValidEndDate,
                RegStartDate = newValidRegStartDate,
                RegEndDate = newValidRegEndDate,
                MaxRegsCount = newValidMaxRegs,
                EventTypeId = newValidEventTypeId
            };
            // Act
            var result = await _sut.Handle(command, CancellationToken.None);
            Event e = _context.Events.Find(validId);

            // Assert
            Assert.NotNull(e);
            Assert.Equal(e.Title, newValidTitle);
            Assert.Equal(e.Description, newValidDescription);
            Assert.Equal(e.StartDate, newValidStartDate);
            Assert.Equal(e.EndDate, newValidEndDate);
            Assert.Equal(e.RegistrationStartDate, newValidRegStartDate);
            Assert.Equal(e.RegistrationEndDate, newValidRegEndDate);
            Assert.Equal((int)e.MaxRegistrations, newValidMaxRegs);
            Assert.Equal(e.EventTypeId, newValidEventTypeId);
        }
        [Fact]
        public async Task Handle_Given_Invalid_EventId_Throws_NotFoundException()
        {
            // Arrange
            var inValidId = 100;
            var newValidTitle = "Updated Event Title";
            var newValidDescription = "This is an updated Event description";
            var newValidStartDate = new DateTime(2020, 10, 1);
            var newValidEndDate = new DateTime(2020, 10, 2);
            var newValidRegStartDate = new DateTime(2020, 9, 1);
            var newValidRegEndDate = new DateTime(2020, 9, 2);
            var newValidMaxRegs = 20;
            var newValidEventTypeId = 2;
            var command = new UpdateEventCommand
            {
                Id = inValidId,
                Title = newValidTitle,
                Description = newValidDescription,
                StartDate = newValidStartDate,
                EndDate = newValidEndDate,
                RegStartDate = newValidRegStartDate,
                RegEndDate = newValidRegEndDate,
                MaxRegsCount = newValidMaxRegs,
                EventTypeId = newValidEventTypeId
            };

            // Act/Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
        }
        [Fact]
        public async Task Handle_Given_Invalid_Title_Throws_ValidationException()
        {
            // Arrange
            var validId = 1;
            var inValidTitle = "";
            var validDescription = "This event was created from a Unit Test.";
            var validStartDate = new DateTime(2020, 3, 1);
            var validEndDate = new DateTime(2020, 3, 2);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = inValidTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = validEventTypeId
            };
            var validator = new UpdateEventCommandValidator(new DateTimeTestProvider()); // manually invoke to test the Validator

            // Act/Assert
            var result = await validator.ValidateAsync(command);
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));
            Assert.Equal(1, ex.Failures.Count);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.Contains(result.Errors, x => x.PropertyName == "Title");
        }
    }
}
