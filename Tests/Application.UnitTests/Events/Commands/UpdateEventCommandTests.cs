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
            await _sut.Handle(command, CancellationToken.None);
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
        public async Task Handle_Given_All_Valid_Values_Updates_Event()
        {
            // Arrange
            var validId = 1;
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
            var validStreet = "123 Anywhere St.";
            var validSuite = "Room #3";
            var validCity = "Yourtown";
            var validState = "MD";
            var validZip = "12345";
            var command = new UpdateEventCommand
            {
                Id = validId,
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
                EventTypeId = validEventTypeId,
                Street = validStreet,
                Suite = validSuite,
                City = validCity,
                State = validState,
                Zip = validZip
            };

            // Act
            var result = await _sut.Handle(command, CancellationToken.None);
            Event e = _context.Events.Find(validId);
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
            Assert.Equal(e.Address.Street, validStreet);
            Assert.Equal(e.Address.Suite, validSuite);
            Assert.Equal(e.Address.City, validCity);
            Assert.Equal(e.Address.State, validState);
            Assert.Equal(e.Address.ZipCode, validZip);
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
        [Theory]
        [InlineData("")]
        [InlineData("          ")]
        [InlineData(null)]
        public async Task Handle_Given_Invalid_Title_Throws_ValidationException(string value)
        {
            // Arrange
            var validId = 1;
            var inValidTitle = value;
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
        [Theory]
        [InlineData("")]
        [InlineData("          ")]
        [InlineData(null)]
        public async Task Handle_Given_Invalid_Description_Throws_ValidationException(string value)
        {
            // Arrange
            var validId = 1;
            var validTitle = "This is a valid title.";
            var inValidDescription = value;
            var validStartDate = new DateTime(2020, 3, 1);
            var validEndDate = new DateTime(2020, 3, 2);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = validTitle,
                Description = inValidDescription,
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
            Assert.Contains(result.Errors, x => x.PropertyName == "Description");
        }
        [Fact]
        public async Task Handle_Given_Invalid_EventTypeId_Handler_Only_Throws_ValidationException()
        {
            // Arrange
            var validId = 1;
            var validTitle = "This is a valid title.";
            var validDescription = "This is a valid description.";           
            var validStartDate = new DateTime(2020, 3, 1);
            var validEndDate = new DateTime(2020, 3, 2);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var inValidEventTypeId = 5;
            var command = new UpdateEventCommand
            {
                Id = validId,
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
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Handle_Given_Invalid_EventTypeId_Validator_Is_Invalid(int value)
        {
            // Arrange
            var validId = 1;
            var validTitle = "This is a valid title.";
            var validDescription = "This is a valid Description";
            var validStartDate = new DateTime(2020, 3, 1);
            var validEndDate = new DateTime(2020, 3, 2);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var inValidEventTypeId = value;
            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = inValidEventTypeId
            };
            var validator = new UpdateEventCommandValidator(new DateTimeTestProvider()); // manually invoke to test the Validator

            // Act/Assert
            var result = await validator.ValidateAsync(command);            
            Assert.False(result.IsValid);            
            Assert.Contains(result.Errors, x => x.PropertyName == "EventTypeId");
        }
        [Fact]
        public async Task Handle_Given_StartDate_In_Past_Throws_ValidationError()
        {
            // Arrange
            var validId = 1;
            var validTitle = "This is a valid title.";
            var validDescription = "This is a valid description";
            var inValidStartDate = new DateTime(2019, 12, 1);
            var validEndDate = new DateTime(2020, 3, 2);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = validTitle,
                Description = validDescription,
                StartDate = inValidStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = validEventTypeId
            };
            var validator = new UpdateEventCommandValidator(new DateTimeTestProvider()); // manually invoke to test the Validator

            // Act/Assert
            var result = await validator.ValidateAsync(command);
            await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));            
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName == "StartDate");
        }
        [Fact]
        public async Task Handle_Given_EndDate_Before_StartDate_Throws_ValidationError()
        {
            // Arrange
            var validId = 1;
            var validTitle = "This is a valid title.";
            var validDescription = "This is a valid Description";
            var validStartDate = new DateTime(2020, 3, 1);
            var inValidEndDate = new DateTime(2020, 2, 28);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = inValidEndDate,
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
            Assert.Contains(result.Errors, x => x.PropertyName == "EndDate");
        }
        [Fact]
        public async Task Handle_Given_RegEndDate_After_StartDate_Throws_ValidationError()
        {
            // Arrange
            var validId = 1;
            var validTitle = "This is a valid title.";
            var validDescription = "This is a valid Description";
            var validStartDate = new DateTime(2020, 3, 1);
            var validEndDate = new DateTime(2020, 3, 2);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var inValidRegEndDate = new DateTime(2020, 3, 2);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = inValidRegEndDate,
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
            Assert.Contains(result.Errors, x => x.PropertyName == "RegEndDate");
        }
        [Fact]
        public async Task Handle_Given_RegStartDate_After_RegEndDate_Throws_ValidationException()
        {            
            // Arrange
            var validId = 1;
            var validTitle = "This is a valid title.";
            var validDescription = "This is a valid Description";
            var validStartDate = new DateTime(2020, 3, 1);
            var validEndDate = new DateTime(2020, 3, 2);
            var inValidRegStartDate = new DateTime(2020, 2, 3);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = inValidRegStartDate,
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
            Assert.Contains(result.Errors, x => x.PropertyName == "RegStartDate");

        }
        [Fact]
        public async Task Handle_Given_MaxRegs_Zero_Throws_ValidattionException()
        {
            // Arrange
            var validId = 1;
            var validTitle = "This is a valid title.";
            var validDescription = "This is a valid Description";
            var validStartDate = new DateTime(2020, 3, 1);
            var validEndDate = new DateTime(2020, 3, 2);
            var validRegStartDate = new DateTime(2020, 2, 3);
            var validRegEndDate = new DateTime(2020, 2, 4);
            var inValidMaxRegs = 0;
            var validEventTypeId = 1;
            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = inValidMaxRegs,
                EventTypeId = validEventTypeId
            };
            var validator = new UpdateEventCommandValidator(new DateTimeTestProvider()); // manually invoke to test the Validator

            // Act/Assert
            var result = await validator.ValidateAsync(command);
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName == "MaxRegsCount");
        }
        [Fact]
        public async Task Handle_Given_MinRegs_GreaterThan_MaxRegs_Throws_ValidationException()
        {
            // Arrange
            var validId = 1;
            var validTitle = "This is a valid title.";
            var validDescription = "This is a valid Description";
            var validStartDate = new DateTime(2020, 3, 1);
            var validEndDate = new DateTime(2020, 3, 2);
            var validRegStartDate = new DateTime(2020, 2, 3);
            var validRegEndDate = new DateTime(2020, 2, 4);
            var validMaxRegs = 10;
            var inValidMinRegs = 20;
            var validEventTypeId = 1;
            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                MinRegsCount = inValidMinRegs,
                EventTypeId = validEventTypeId

            };
            var validator = new UpdateEventCommandValidator(new DateTimeTestProvider()); // manually invoke to test the Validator

            // Act/Assert
            var result = await validator.ValidateAsync(command);
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName == "MinRegsCount");
        }
        [Fact]
        public async Task Validate_Given_Invalid_EventTypeId_Validator_Is_Invalid()
        {
            // Arrange
            var validId = 1;
            var validTitle = "This is a valid title.";
            var validDescription = "This is a valid Description";
            var validStartDate = new DateTime(2020, 3, 1);
            var validEndDate = new DateTime(2020, 3, 2);
            var validRegStartDate = new DateTime(2020, 2, 3);
            var validRegEndDate = new DateTime(2020, 2, 4);
            var validMaxRegs = 10;
            var inValidEventTypeId = 0;
            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,                
                EventTypeId = inValidEventTypeId

            };
            var validator = new UpdateEventCommandValidator(new DateTimeTestProvider()); // manually invoke to test the Validator

            // Act/Assert
            var result = await validator.ValidateAsync(command);            
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName == "EventTypeId");
        }
        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        public async Task Handle_Given_Invalid_EventTypeId_Throws_ValidationException(int value)
        {
            // Arrange
            var validId = 1;
            var validTitle = "This is a valid title.";
            var validDescription = "This is a valid Description";
            var validStartDate = new DateTime(2020, 3, 1);
            var validEndDate = new DateTime(2020, 3, 2);
            var validRegStartDate = new DateTime(2020, 2, 3);
            var validRegEndDate = new DateTime(2020, 2, 4);
            var validMaxRegs = 10;
            var inValidEventTypeId = value;
            var command = new UpdateEventCommand
            {
                Id = validId,
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
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Validate_Given_Invalid_EventSeriesId_Validator_Is_Invalid(int value)
        {
            // Arrange
            var validId = 1;
            var validTitle = "This is a valid title.";
            var validDescription = "This is a valid Description";
            var validStartDate = new DateTime(2020, 3, 1);
            var validEndDate = new DateTime(2020, 3, 2);
            var validRegStartDate = new DateTime(2020, 2, 3);
            var validRegEndDate = new DateTime(2020, 2, 4);
            var validMaxRegs = 10;
            var inValidEventTypeId = 0;
            var inValidEventSeriesId = value;
            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = inValidEventTypeId,
                EventSeriesId = inValidEventSeriesId

            };
            var validator = new UpdateEventCommandValidator(new DateTimeTestProvider()); // manually invoke to test the Validator

            // Act/Assert
            var result = await validator.ValidateAsync(command);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName == "EventSeriesId");
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(100)]
        public async Task Handle_Given_Invalid_EventSeriesId_Throws_ValidationException(int value)
        {
            // Arrange
            var validId = 1;
            var validTitle = "This is a valid title.";
            var validDescription = "This is a valid Description";
            var validStartDate = new DateTime(2020, 3, 1);
            var validEndDate = new DateTime(2020, 3, 2);
            var validRegStartDate = new DateTime(2020, 2, 3);
            var validRegEndDate = new DateTime(2020, 2, 4);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var inValidEventSeriesId = value;
            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = validEventTypeId,
                EventSeriesId = inValidEventSeriesId
            };
            // Act/Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));
            Assert.Equal(1, ex.Failures.Count);
        }
        [Theory]
        [InlineData("")]
        [InlineData("          ")]
        public async Task Handle_Given_Invalid_Street_Throws_ValidationException(string value)
        {
            // Arrange
            var validId = 1;
            var validTitle = "Event Created from Unit Tests.";
            var validDescription = "This event was created from a Unit Test.";
            var validStartDate = new DateTime(2020, 3, 3);
            var validEndDate = new DateTime(2020, 3, 4);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var inValidStreet = value;
            var validSuite = "Room #3";
            var validCity = "Yourtown";
            var validState = "MD";
            var validZip = "12345";

            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = validEventTypeId,
                Street = inValidStreet,
                Suite = validSuite,
                City = validCity,
                State = validState,
                Zip = validZip
            };

            // Act/Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));
            Assert.Equal(1, ex.Failures.Count);
        }
        [Theory]
        [InlineData("")]
        [InlineData("          ")]
        public async Task Handle_Given_Invalid_City_Throws_ValidationException(string value)
        {
            // Arrange
            var validId = 1;
            var validTitle = "Event Created from Unit Tests.";
            var validDescription = "This event was created from a Unit Test.";
            var validStartDate = new DateTime(2020, 3, 3);
            var validEndDate = new DateTime(2020, 3, 4);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var validStreet = "123 Anywhere St.";
            var validSuite = "Room #3";
            var inValidCity = value;
            var validState = "MD";
            var validZip = "12345";

            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = validEventTypeId,
                Street = validStreet,
                Suite = validSuite,
                City = inValidCity,
                State = validState,
                Zip = validZip
            };

            // Act/Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));
            Assert.Equal(1, ex.Failures.Count);
        }
        [Theory]
        [InlineData("")]
        [InlineData("          ")]
        public async Task Handle_Given_Invalid_State_Throws_ValidationException(string value)
        {
            // Arrange
            var validId = 1;
            var validTitle = "Event Created from Unit Tests.";
            var validDescription = "This event was created from a Unit Test.";
            var validStartDate = new DateTime(2020, 3, 3);
            var validEndDate = new DateTime(2020, 3, 4);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var validStreet = "123 Anywhere St.";
            var validSuite = "Room #3";
            var validCity = "Yourtown";
            var inValidState = value;
            var validZip = "12345";

            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = validEventTypeId,
                Street = validStreet,
                Suite = validSuite,
                City = validCity,
                State = inValidState,
                Zip = validZip
            };

            // Act/Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));
            Assert.Equal(1, ex.Failures.Count);
        }
        [Theory]
        [InlineData("")]
        [InlineData("          ")]
        public async Task Handle_Given_Invalid_Zip_Throws_ValidationException(string value)
        {
            // Arrange
            var validId = 1;
            var validTitle = "Event Created from Unit Tests.";
            var validDescription = "This event was created from a Unit Test.";
            var validStartDate = new DateTime(2020, 3, 3);
            var validEndDate = new DateTime(2020, 3, 4);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var validStreet = "123 Anywhere St.";
            var validSuite = "Room #3";
            var validCity = "Yourtown";
            var validState = "MD";
            var inValidZip = value;

            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = validEventTypeId,
                Street = validStreet,
                Suite = validSuite,
                City = validCity,
                State = validState,
                Zip = inValidZip
            };

            // Act/Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(command, CancellationToken.None));
            Assert.Equal(1, ex.Failures.Count);
        }
        [Theory]
        [InlineData("")]
        [InlineData("          ")]
        public async Task Validate_Given_Invalid_Street_Validator_Is_Invalid(string value)
        {
            // Arrange
            var validId = 1;
            var validTitle = "Event Created from Unit Tests.";
            var validDescription = "This event was created from a Unit Test.";
            var validStartDate = new DateTime(2020, 3, 3);
            var validEndDate = new DateTime(2020, 3, 4);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var inValidStreet = value;
            var validSuite = "Room #3";
            var validCity = "Yourtown";
            var validState = "MD";
            var validZip = "12345";

            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = validEventTypeId,
                Street = inValidStreet,
                Suite = validSuite,
                City = validCity,
                State = validState,
                Zip = validZip
            };
            var validator = new UpdateEventCommandValidator(new DateTimeTestProvider());

            // Act/Assert
            var result = await validator.ValidateAsync(command);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.Contains(result.Errors, x => x.PropertyName == "Street");
        }
        [Theory]
        [InlineData("")]
        [InlineData("          ")]
        public async Task Validate_Given_Invalid_City_Validator_Is_Invalid(string value)
        {
            // Arrange
            var validId = 1;
            var validTitle = "Event Created from Unit Tests.";
            var validDescription = "This event was created from a Unit Test.";
            var validStartDate = new DateTime(2020, 3, 3);
            var validEndDate = new DateTime(2020, 3, 4);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var validStreet = "123 Anywhere St.";
            var validSuite = "Room #3";
            var inValidCity = value;
            var validState = "MD";
            var validZip = "12345";

            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = validEventTypeId,
                Street = validStreet,
                Suite = validSuite,
                City = inValidCity,
                State = validState,
                Zip = validZip
            };
            var validator = new UpdateEventCommandValidator(new DateTimeTestProvider());

            // Act/Assert
            var result = await validator.ValidateAsync(command);
            Assert.False(result.IsValid);
            Assert.Equal(1, result.Errors.Count);
            Assert.Contains(result.Errors, x => x.PropertyName == "City");
        }
        [Theory]
        [InlineData("")]
        [InlineData("          ")]
        [InlineData("A")]
        [InlineData("ABC")]
        public async Task Validate_Given_Invalid_State_Validator_Is_Invalid(string value)
        {
            // Arrange
            var validId = 1;
            var validTitle = "Event Created from Unit Tests.";
            var validDescription = "This event was created from a Unit Test.";
            var validStartDate = new DateTime(2020, 3, 3);
            var validEndDate = new DateTime(2020, 3, 4);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var validStreet = "123 Anywhere St.";
            var validSuite = "Room #3";
            var validCity = "Yourtown";
            var inValidState = value;
            var validZip = "12345";

            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = validEventTypeId,
                Street = validStreet,
                Suite = validSuite,
                City = validCity,
                State = inValidState,
                Zip = validZip
            };
            var validator = new UpdateEventCommandValidator(new DateTimeTestProvider());

            // Act/Assert
            var result = await validator.ValidateAsync(command);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName == "State");
        }
        [Theory]
        [InlineData("")]
        [InlineData("          ")]
        [InlineData("A")]
        [InlineData("ABC")]
        [InlineData("A1234")]
        public async Task Validate_Given_Invalid_Zip_Validator_Is_Invalid(string value)
        {
            // Arrange
            var validId = 1;
            var validTitle = "Event Created from Unit Tests.";
            var validDescription = "This event was created from a Unit Test.";
            var validStartDate = new DateTime(2020, 3, 3);
            var validEndDate = new DateTime(2020, 3, 4);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var validStreet = "123 Anywhere St.";
            var validSuite = "Room #3";
            var validCity = "Yourtown";
            var validState = "MD";
            var inValidZip = value;

            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = validEventTypeId,
                Street = validStreet,
                Suite = validSuite,
                City = validCity,
                State = validState,
                Zip = inValidZip
            };
            var validator = new UpdateEventCommandValidator(new DateTimeTestProvider());

            // Act/Assert
            var result = await validator.ValidateAsync(command);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName == "Zip");
        }
        [Theory]
        [InlineData("12345")]
        [InlineData("00000")]
        [InlineData("99991")]
        public async Task Validate_Given_Valid_Zip_Validator_Is_Valid(string value)
        {
            // Arrange
            var validId = 1;
            var validTitle = "Event Created from Unit Tests.";
            var validDescription = "This event was created from a Unit Test.";
            var validStartDate = new DateTime(2020, 3, 3);
            var validEndDate = new DateTime(2020, 3, 4);
            var validRegStartDate = new DateTime(2020, 2, 1);
            var validRegEndDate = new DateTime(2020, 2, 2);
            var validMaxRegs = 10;
            var validEventTypeId = 1;
            var validStreet = "123 Anywhere St.";
            var validSuite = "Room #3";
            var validCity = "Yourtown";
            var validState = "MD";
            var validZip = value;

            var command = new UpdateEventCommand
            {
                Id = validId,
                Title = validTitle,
                Description = validDescription,
                StartDate = validStartDate,
                EndDate = validEndDate,
                RegStartDate = validRegStartDate,
                RegEndDate = validRegEndDate,
                MaxRegsCount = validMaxRegs,
                EventTypeId = validEventTypeId,
                Street = validStreet,
                Suite = validSuite,
                City = validCity,
                State = validState,
                Zip = validZip
            };
            var validator = new UpdateEventCommandValidator(new DateTimeTestProvider());

            // Act/Assert
            var result = await validator.ValidateAsync(command);
            Assert.True(result.IsValid);
        }
    }
}
