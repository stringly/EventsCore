using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Events.Commands.CreateEvent;
using EventsCore.Application.UnitTests.Common;
using EventsCore.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.Application.UnitTests.Events.Commands
{
    public class CreateEventCommandTests : CommandTestBase
    {
        private readonly CreateEventCommandHandler _sut;
        public CreateEventCommandTests() : base()
        {
            _sut = new CreateEventCommandHandler(_context, new DateTimeTestProvider());
        }
        [Fact]
        public async Task Handle_Given_Minimum_Valid_Values_Creates_Event()
        {
            // Arrange
            var validTitle = "Event Created from Unit Tests.";
            var validDescription = "This event was created from a Unit Test.";
            var validStartDate = new DateTime(2020, 3, 1);
            var validEndDate = new DateTime(2020, 3, 2);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var command = new CreateEventCommand
            {
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = validEventTypeId
            };

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);            
            Event e = _context.Events.FirstOrDefault(x => x.Title == validTitle);
            // Assert
            Assert.NotNull(e);
            Assert.Equal(e.Title, validTitle);
            Assert.Equal(e.Description, validDescription);
            Assert.Equal(e.StartDate, validStartDate);
            Assert.Equal(e.EndDate, validEndDate);
            Assert.Equal(e.RegistrationStartDate, validRegStartDate);
            Assert.Equal(e.RegistrationEndDate, validRegEndDate);
            Assert.Equal((int)e.MaxRegistrations, validMaxRegs);
            Assert.Equal(e.EventTypeId, validEventTypeId);


        }
        [Fact]
        public async Task Handle_Given_All_Valid_Values_Creates_Event()
        {
            // Arrange
            var validTitle = "Event Created from Unit Tests.";
            var validDescription = "This event was created from a Unit Test.";
            var validStartDate = new DateTime(2020, 3, 1);
            var validEndDate = new DateTime(2020, 3, 2);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var validMinRegs = 1;
            var validMaxStandbyCount = 10;
            var validEventTypeId = 1;
            var validEventSeriesId = 1;
            var command = new CreateEventCommand
            {
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                MinRegsCount = validMinRegs,
                MaxStandbyCount = validMaxStandbyCount,
                EventSeriesId = validEventSeriesId,
                EventTypeId = validEventTypeId
            };

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);
            Event e = _context.Events.FirstOrDefault(x => x.Title == validTitle);
            // Assert
            Assert.NotNull(e);
            Assert.Equal(e.Title, validTitle);
            Assert.Equal(e.Description, validDescription);
            Assert.Equal(e.StartDate, validStartDate);
            Assert.Equal(e.EndDate, validEndDate);
            Assert.Equal(e.RegistrationStartDate, validRegStartDate);
            Assert.Equal(e.RegistrationEndDate, validRegEndDate);
            Assert.Equal((int)e.MaxRegistrations, validMaxRegs);
            Assert.Equal((int)e.Rules.MinRegistrations, validMinRegs);
            Assert.Equal((int)e.Rules.MaxStandbyRegistrations, validMaxStandbyCount);
            Assert.Equal(e.EventSeriesId, validEventSeriesId);
            Assert.Equal(e.EventTypeId, validEventTypeId);
        }
        [Fact]
        public async Task Handle_Given_Invalid_Title_Throws_ValidationException()
        {
            // Arrange
            var inValidTitle = "";
            var validDescription = "This event was created from a Unit Test.";
            var validStartDate = new DateTime(2020, 3, 1);
            var validEndDate = new DateTime(2020, 3, 2);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var command = new CreateEventCommand
            {
                Title = inValidTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = validEventTypeId
            };
            var validator = new CreateEventCommandValidator(new DateTimeTestProvider()); // manually invoke to test the Validator

            // Act/Assert
            var result = await validator.ValidateAsync(command);
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));
            Assert.Equal(1, ex.Failures.Count);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.Contains(result.Errors, x => x.PropertyName == "Title");
        }
        
        [Fact]
        public async Task Handle_Given_Invalid_Description_Throws_ValidationException()
        {
            // Arrange
            var validTitle = "Event Created from Unit Tests.";
            var inValidDescription = "";
            var validStartDate = new DateTime(2020, 3, 1);
            var validEndDate = new DateTime(2020, 3, 2);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var command = new CreateEventCommand
            {
                Title = validTitle,
                Description = inValidDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = validEventTypeId
            };
            var validator = new CreateEventCommandValidator(new DateTimeTestProvider()); // manually invoke to test the Validator

            // Act/Assert
            var result = await validator.ValidateAsync(command);
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));
            Assert.Equal(1, ex.Failures.Count);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.Contains(result.Errors, x => x.PropertyName == "Description");
        }
        [Fact]
        public async Task Handle_Given_Invalid_EventTypeId_Throws_ValidationException()
        {
            // Arrange
            var validTitle = "Event Created from Unit Tests.";
            var validDescription = "This event was created from a Unit Test.";
            var validStartDate = new DateTime(2020, 3, 1);
            var validEndDate = new DateTime(2020, 3, 2);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var inValidEventTypeId = 20;
            var command = new CreateEventCommand
            {
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = inValidEventTypeId
            };

            // Act/Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));
            Assert.Equal(1, ex.Failures.Count);
        }
        [Fact]
        public async Task Handle_Given_StartDate_InPast_Throws_ValidationException()
        {
            // Arrange
            var validTitle = "Event Created from Unit Tests.";
            var validDescription = "This event was created from a Unit Test.";
            var inValidStartDate = new DateTime(2019, 3, 1);
            var validEndDate = new DateTime(2020, 3, 2);
            var validRegStartDate = new DateTime(2019, 1, 1);
            var validRegEndDate = new DateTime(2019, 2, 1);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var command = new CreateEventCommand
            {
                Title = validTitle,
                Description = validDescription,
                StartDate = inValidStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = validEventTypeId
            };
            var validator = new CreateEventCommandValidator(new DateTimeTestProvider()); // manually invoke to test the Validator
            // Act/Assert
            var result = await validator.ValidateAsync(command);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.Contains(result.Errors, x => x.PropertyName == "StartDate");
        }
        [Fact]
        public async Task Handle_Given_StartDate_After_EndDate_Throws_ValidationException()
        {
            // Arrange
            var validTitle = "Event Created from Unit Tests.";
            var validDescription = "This event was created from a Unit Test.";
            var inValidStartDate = new DateTime(2020, 3, 3);
            var validEndDate = new DateTime(2020, 3, 2);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var command = new CreateEventCommand
            {
                Title = validTitle,
                Description = validDescription,
                StartDate = inValidStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = validEventTypeId
            };
            var validator = new CreateEventCommandValidator(new DateTimeTestProvider()); // manually invoke to test the Validator
            // Act/Assert
            var result = await validator.ValidateAsync(command);
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));
            Assert.Equal(1, ex.Failures.Count);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.Contains(result.Errors, x => x.PropertyName == "EndDate");
        }
        [Fact]
        public async Task Handle_Given_RegEndDate_After_EventStartDate_Throws_ValidationException()
        {
            // Arrange
            var validTitle = "Event Created from Unit Tests.";
            var validDescription = "This event was created from a Unit Test.";
            var validStartDate = new DateTime(2020, 3, 1);
            var validEndDate = new DateTime(2020, 3, 2);
            var validRegStartDate = new DateTime(2020, 1, 1);
            var inValidRegEndDate = new DateTime(2020, 3, 2);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var command = new CreateEventCommand
            {
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = inValidRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = validEventTypeId
            };
            var validator = new CreateEventCommandValidator(new DateTimeTestProvider()); // manually invoke to test the Validator
            // Act/Assert
            var result = await validator.ValidateAsync(command);
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));
            Assert.Equal(1, ex.Failures.Count);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.Contains(result.Errors, x => x.PropertyName == "RegEndDate");
        }
        [Fact]
        public async Task Handle_Given_RegEndDate_Before_RegStartDate_Throw_ValidationException()
        {
            // Arrange
            var validTitle = "Event Created from Unit Tests.";
            var validDescription = "This event was created from a Unit Test.";
            var validStartDate = new DateTime(2020, 3, 3);
            var validEndDate = new DateTime(2020, 3, 4);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var inValidRegEndDate = new DateTime(2020, 1, 1);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var command = new CreateEventCommand
            {
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = inValidRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = validEventTypeId
            };
            var validator = new CreateEventCommandValidator(new DateTimeTestProvider());

            // Act/Assert
            var result = await validator.ValidateAsync(command);
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));
            Assert.Equal(1, ex.Failures.Count);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.Contains(result.Errors, x => x.PropertyName == "RegStartDate");
        }

    }
}
