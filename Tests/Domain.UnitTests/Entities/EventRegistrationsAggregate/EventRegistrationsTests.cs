using EventsCore.Domain.Common;
using EventsCore.Domain.Entities.EventRegistrationsAggregate;
using EventsCore.Domain.Exceptions.EventRegistrationsAggregate;
using EventsCore.Domain.UnitTests.Common;
using EventsCore.Domain.ValueObjects;
using System;
using System.Linq;
using Xunit;

namespace EventsCore.Domain.UnitTests.Entities.EventRegistrationsAggregate
{
    public class EventRegistrationsTests
    {
        private readonly IDateTime _dateTime;
        public EventRegistrationsTests()
        {
            _dateTime = new DateTimeTestProvider();
        }
        [Fact]
        public void EventRegistrations_Given_Valid_Values_Is_Valid()
        {
            // Arrange
            int eventId = 1;
            var EventDates = new EventDates(
                new DateTime(3000, 2, 1),
                new DateTime(3000, 2, 2),
                new DateTime(3000, 1, 1),
                new DateTime(3000, 1, 2),
                _dateTime);
            var RegistrationRules = new EventRegistrationRules(10);

            // Act
            var evRegs = new EventRegistrations(eventId, EventDates, RegistrationRules, _dateTime);

            // Assert
            Assert.Equal(1, evRegs.Id);
            Assert.Equal(EventDates, evRegs.EventDates);
            Assert.Equal(RegistrationRules, evRegs.Rules);
        }

        [Fact]
        public void Should_Throw_EventRegistrationAggregateArgumentException_For_EventId_Out_Of_Range()
        {
            // Arrange
            int eventId = 0;
            var EventDates = new EventDates(
                new DateTime(3000, 2, 1),
                new DateTime(3000, 2, 2),
                new DateTime(3000, 1, 1),
                new DateTime(3000, 1, 2),                
                _dateTime);
            var RegistrationRules = new EventRegistrationRules(10);

            // Act/Assert
            Assert.Throws<EventRegistrationAggregateArgumentException>(() => new EventRegistrations(eventId, EventDates, RegistrationRules, _dateTime));
        }
        [Fact]
        public void Should_Throw_EventRegistrationAggregateArgumentException_For_Null_EventDates()
        {
            // Arrange
            int eventId = 1;
            EventDates eventDates = null;
            var registrationRules = new EventRegistrationRules(10);

            // Act/Assert
            Assert.Throws<EventRegistrationAggregateArgumentException>(() => new EventRegistrations(eventId, eventDates, registrationRules, _dateTime));
        }
        [Fact]
        public void Should_Throw_EventRegistrationAggregateArgumentException_For_Null_RegistrationRules()
        {
            // Arrange
            int eventId = 1;
            var eventDates = new EventDates(
                new DateTime(3000, 2, 1),
                new DateTime(3000, 2, 2),
                new DateTime(3000, 1, 1),
                new DateTime(3000, 1, 2),
                _dateTime);
            EventRegistrationRules registrationRules = null;

            // Act/Assert
            Assert.Throws<EventRegistrationAggregateArgumentException>(() => new EventRegistrations(eventId, eventDates, registrationRules, _dateTime));
        }
        [Fact]
        public void Given_Future_RegistrationPeriodStartDate_IsAcceptingRegistrations_Returns_False()
        {
            // Arrange
            int eventId = 1;
            var eventDates = new EventDates(
                new DateTime(3000, 2, 1),
                new DateTime(3000, 2, 2),
                new DateTime(3000, 1, 1), // registration period start date is in future
                new DateTime(3000, 1, 2),
                _dateTime);
            var registrationRules = new EventRegistrationRules(10);
            var eventRegistrations = new EventRegistrations(eventId, eventDates, registrationRules, _dateTime);

            // Act
            var result = eventRegistrations.IsAcceptingRegistrations;

            // Assert
            Assert.False(result);            
        }
        [Fact]
        public void Given_Max_Registrations_IsAcceptingRegistrations_Returns_False()
        {
            // Arrange
            int eventId = 1;
            var eventDates = new EventDates(
                new DateTime(3000, 2, 1),
                new DateTime(3000, 2, 2),
                new DateTime(2019, 12, 1), // reg open date needs to be before test date of 1/1/2020 to ensure registration period open
                new DateTime(3000, 1, 2),
                _dateTime);
            var registrationRules = new EventRegistrationRules(1); // allow 1 registration
            var eventRegistrations = new EventRegistrations(eventId, eventDates, registrationRules, _dateTime); // construct aggregate
            eventRegistrations.RegisterUser(1, "Test User", "TestUser@mail.com", ""); // register user with Id = 1
            Assert.Equal(1, eventRegistrations.Registrations.Count); // ensure registration was added
            eventRegistrations.AcceptRegistrationByUserId(1); // accept registration to ensure the "CurrentAttendeesCount" counts it
            // Act
            var result = eventRegistrations.IsAcceptingRegistrations;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Can_Remove_Current_Registration_By_UserId()
        {
            // Arrange
            int eventId = 1;
            var eventDates = new EventDates(
                new DateTime(3000, 2, 1),
                new DateTime(3000, 2, 2),
                new DateTime(2019, 12, 1), // reg open date needs to be before test date of 1/1/2020 to ensure registration period open
                new DateTime(3000, 1, 2),
                _dateTime);
            var registrationRules = new EventRegistrationRules(1);
            var eventRegistrations = new EventRegistrations(eventId, eventDates, registrationRules, _dateTime); // construct aggregate
            eventRegistrations.RegisterUser(1, "Test User", "TestUser@mail.com", ""); // register user with Id = 1
            Assert.Equal(1, eventRegistrations.Registrations.Count); // ensure registration was added
            
            // Act
            eventRegistrations.DeleteRegistrationByUserId(1);

            // Assert
            Assert.Empty(eventRegistrations.Registrations);
        }
        [Fact]
        public void Should_Throw_EventRegistrationAggregateArgumentException_When_Unregistering_UserId_Not_Found()
        {
            // Arrange
            int eventId = 1;
            var eventDates = new EventDates(
                new DateTime(3000, 2, 1),
                new DateTime(3000, 2, 2),
                new DateTime(2019, 12, 1), // reg open date needs to be before test date of 1/1/2020 to ensure registration period open
                new DateTime(3000, 1, 2),
                _dateTime);
            var registrationRules = new EventRegistrationRules(1);
            var eventRegistrations = new EventRegistrations(eventId, eventDates, registrationRules, _dateTime); // construct aggregate
            eventRegistrations.RegisterUser(1, "Test User", "TestUser@mail.com", ""); // register user with Id = 1
            Assert.Equal(1, eventRegistrations.Registrations.Count); // ensure registration was added

            // Act/Assert
            Assert.Throws<EventRegistrationAggregateArgumentException>(() => eventRegistrations.DeleteRegistrationByUserId(2));

        }
        [Fact]
        public void Should_Throw_EventRegistrationAggregateArgumentException_When_Registering_UserId_Already_Registered()
        {
            // Arrange
            int eventId = 1;
            var eventDates = new EventDates(
                new DateTime(3000, 2, 1),
                new DateTime(3000, 2, 2),
                new DateTime(2019, 12, 1), // reg open date needs to be before test date of 1/1/2020 to ensure registration period open
                new DateTime(3000, 1, 2),
                _dateTime);
            var registrationRules = new EventRegistrationRules(1);
            var eventRegistrations = new EventRegistrations(eventId, eventDates, registrationRules, _dateTime); // construct aggregate
            eventRegistrations.RegisterUser(1, "Test User", "TestUser@mail.com", ""); // register user with Id = 1
            Assert.Equal(1, eventRegistrations.Registrations.Count); // ensure registration was added

            // Act/Assert
            Assert.Throws<EventRegistrationAggregateInvalidOperationException>(() => eventRegistrations.RegisterUser(1, "Test", "Test@test", ""));
        }
        [Fact]
        public void Given_Dates_Event_IsExpired_Is_Correct()
        {
            // Arrange            
            var expiredEventDates = new EventDates(
                new DateTime(2019, 1, 1), // Start/End date should be in past
                new DateTime(2019, 1, 2),
                new DateTime(2018, 1, 1),
                new DateTime(2018, 1, 2),
                new DateTimeTestProvider(new DateTime(2018, 12, 1))); //inject custom IDateTime to allow entry of an Event Start Date in the past
            var nonExpiredEventDates = new EventDates(
                new DateTime(2020, 2, 1), 
                new DateTime(2020, 2, 2),
                new DateTime(2020, 1, 1),
                new DateTime(2020, 1, 2),
                _dateTime);
            var registrationRules = new EventRegistrationRules(10);
            var expiredEventRegistrations = new EventRegistrations(1, expiredEventDates, registrationRules, _dateTime);
            var nonExpiredEventRegistrations = new EventRegistrations(1, nonExpiredEventDates, registrationRules, _dateTime);

            // Act/Assert
            Assert.True(expiredEventRegistrations.IsExpired);
            Assert.False(nonExpiredEventRegistrations.IsExpired);
        }
        [Fact]
        public void Given_Dates_Event_IsActive_Is_Correct()
        {
            // Arrange
            var activeEventDates = new EventDates(
                new DateTime(2020, 1, 1),
                new DateTime(2020, 1, 2),
                new DateTime(2019, 12, 1), 
                new DateTime(2019, 12, 30),
                new DateTimeTestProvider(new DateTime(2019, 11, 1)));
            var inActiveEventDates = new EventDates(
                new DateTime(2020, 2, 1),
                new DateTime(2020, 2, 2),
                new DateTime(2020, 1, 1),
                new DateTime(2020, 1, 2),
                new DateTimeTestProvider(new DateTime(2019, 11, 1)));
            var registrationRules = new EventRegistrationRules(10);
            var activeEventRegistrations = new EventRegistrations(1, activeEventDates, registrationRules, _dateTime);
            var inActiveEventRegistrations = new EventRegistrations(1, inActiveEventDates, registrationRules, _dateTime);

            // Act/Assert
            Assert.True(activeEventRegistrations.IsActive);
            Assert.False(inActiveEventRegistrations.IsActive);
        }

        [Fact]
        public void Should_Throw_EventRegistrationAggregateArgumentException_When_Rejecting_Registration_UserId_Not_Found()
        {
            // Arrange
            var eventDates = new EventDates(
                new DateTime(2020, 2, 1), 
                new DateTime(2020, 2, 2),
                new DateTime(2020, 1, 1),
                new DateTime(2020, 1, 2),
                _dateTime);
            var registrationRules = new EventRegistrationRules(10);
            var eventRegistrations = new EventRegistrations(1, eventDates, registrationRules, _dateTime);
            eventRegistrations.RegisterUser(1, "Test User", "TestUser@mail.com", ""); // register user with Id = 1
            Assert.Equal(1, eventRegistrations.Registrations.Count); // ensure registration was added
            // Act/Assert
            Assert.Throws<EventRegistrationAggregateArgumentException>(() => eventRegistrations.RejectRegistrationByUserId(2));
        }
        [Fact]
        public void Can_Reject_Registration_By_UserId()
        {
            // Arrange
            var eventDates = new EventDates(
                new DateTime(2020, 2, 1),
                new DateTime(2020, 2, 2),
                new DateTime(2020, 1, 1),
                new DateTime(2020, 1, 2),
                _dateTime);
            var registrationRules = new EventRegistrationRules(10);
            var eventRegistrations = new EventRegistrations(1, eventDates, registrationRules, _dateTime);
            eventRegistrations.RegisterUser(1, "Test User", "TestUser@mail.com", ""); // register user with Id = 1
            Assert.Equal(1, eventRegistrations.Registrations.Count); // ensure registration was added
            
            // Act
            eventRegistrations.RejectRegistrationByUserId(1);
            Registration rejected = eventRegistrations.Registrations.FirstOrDefault(x => x.UserId == 1);

            // Assert
            Assert.Equal(RegistrationStatus.Rejected, rejected.Status);
            
        }
        [Fact]
        public void Can_Accept_Registration_By_UserId()
        {
            // Arrange
            var eventDates = new EventDates(
                new DateTime(2020, 2, 1),
                new DateTime(2020, 2, 2),
                new DateTime(2020, 1, 1),
                new DateTime(2020, 1, 2),
                _dateTime);
            var registrationRules = new EventRegistrationRules(10);
            var eventRegistrations = new EventRegistrations(1, eventDates, registrationRules, _dateTime);
            eventRegistrations.RegisterUser(1, "Test User", "TestUser@mail.com", ""); // register user with Id = 1
            Assert.Equal(1, eventRegistrations.Registrations.Count); // ensure registration was added

            // Act
            eventRegistrations.AcceptRegistrationByUserId(1);
            Registration accepted = eventRegistrations.Registrations.FirstOrDefault(x => x.UserId == 1);

            // Assert
            Assert.Equal(RegistrationStatus.Accepted, accepted.Status);

        }
        [Fact]
        public void Current_Attendees_Count_Is_Correct()
        {
            // Arrange
            var eventDates = new EventDates(
                new DateTime(2020, 2, 1),
                new DateTime(2020, 2, 2),
                new DateTime(2020, 1, 1),
                new DateTime(2020, 1, 2),
                _dateTime);
            var registrationRules = new EventRegistrationRules(10);
            var eventRegistrations = new EventRegistrations(1, eventDates, registrationRules, _dateTime);
            eventRegistrations.RegisterUser(1, "Test User", "TestUser@mail.com", ""); // register user with Id = 1
            eventRegistrations.RegisterUser(2, "Second Test User", "TestUserTwo@mail.com", ""); // register a second user
            Assert.Equal(2, eventRegistrations.Registrations.Count); // ensure registrations were added

            // Act
            eventRegistrations.AcceptRegistrationByUserId(1); // accept only 1 registration

            // Assert
            Assert.Equal(1, eventRegistrations.CurrentAttendeesCount);
        }
        [Fact]
        public void IsStandbyAvailable_Returns_Correct_Values()
        {
            // Arrange
            var eventDates = new EventDates(
                new DateTime(2020, 2, 1),
                new DateTime(2020, 2, 2),
                new DateTime(2020, 1, 1),
                new DateTime(2020, 1, 2),
                _dateTime);
            var registrationRulesNoStandby = new EventRegistrationRules(10); // don't pass a StandbyCount
            var registrationRulesWithStandby = new EventRegistrationRules(10, 1, 1);
            var eventRegistrationsNoStandby = new EventRegistrations(1, eventDates, registrationRulesNoStandby, _dateTime);
            var eventRegistrationsWithStandby = new EventRegistrations(2, eventDates, registrationRulesWithStandby, _dateTime);

            // Act
            var shouldBeFalse = eventRegistrationsNoStandby.IsStandByAvailable;
            var shouldBeTrue = eventRegistrationsWithStandby.IsStandByAvailable;

            // Assert
            Assert.False(shouldBeFalse);
            Assert.True(shouldBeTrue);
        }
        [Fact]
        public void Should_Throw_EventAggregateInvalidOperationException_For_Standby_Not_Available()
        {
            // Arrange
            var eventDates = new EventDates(
                new DateTime(2020, 2, 1),
                new DateTime(2020, 2, 2),
                new DateTime(2020, 1, 1),
                new DateTime(2020, 1, 2),
                _dateTime);
            var registrationRules = new EventRegistrationRules(10); // don't pass a StandbyCount
            var eventRegistrations = new EventRegistrations(1, eventDates, registrationRules, _dateTime);

            // Act/Assert
            Assert.Throws<EventRegistrationAggregateInvalidOperationException>(() => eventRegistrations.StandbyRegistrationByUserId(1));
        }
        [Fact]
        public void Should_Throw_EventAggregateInvalidOperationException_For_Standby_When_Standby_Full()
        {
            // Arrange
            var eventDates = new EventDates(
                new DateTime(2020, 2, 1),
                new DateTime(2020, 2, 2),
                new DateTime(2020, 1, 1),
                new DateTime(2020, 1, 2),
                _dateTime);
            var registrationRules = new EventRegistrationRules(10, 1, 1);
            var eventRegistrations = new EventRegistrations(1, eventDates, registrationRules, _dateTime);
            eventRegistrations.RegisterUser(1, "Test User", "TestUser@mail.com", ""); // register user with Id = 1
            eventRegistrations.RegisterUser(2, "Second Test User", "TestUserTwo@mail.com", ""); // register a second user
            Assert.Equal(2, eventRegistrations.Registrations.Count); // ensure registrations were added
            eventRegistrations.StandbyRegistrationByUserId(1); // move 1 registration to standby
            Assert.Equal(1, eventRegistrations.CurrentStandbyCount); // ensure registration was moved to standby

            // Assert
            Assert.Throws<EventRegistrationAggregateInvalidOperationException>(() => eventRegistrations.StandbyRegistrationByUserId(2));
        }
        [Fact]
        public void Can_Standby_Registration_By_UserId()
        {
            // Arrange
            var eventDates = new EventDates(
                new DateTime(2020, 2, 1),
                new DateTime(2020, 2, 2),
                new DateTime(2020, 1, 1),
                new DateTime(2020, 1, 2),
                _dateTime);
            var registrationRules = new EventRegistrationRules(10, 1, 1);
            var eventRegistrations = new EventRegistrations(1, eventDates, registrationRules, _dateTime);
            eventRegistrations.RegisterUser(1, "Test User", "TestUser@mail.com", ""); // register user with Id = 1
            eventRegistrations.RegisterUser(2, "Second Test User", "TestUserTwo@mail.com", ""); // register a second user
            Assert.Equal(2, eventRegistrations.Registrations.Count); // ensure registrations were added

            // Act
            eventRegistrations.StandbyRegistrationByUserId(1);
            Registration standby = eventRegistrations.Registrations.FirstOrDefault(x => x.UserId == 1);

            // Assert
            Assert.Equal(RegistrationStatus.Standby, standby.Status);
        }
        [Fact]
        public void Current_Standby_Count_Is_Correct()
        {
            // Arrange
            var eventDates = new EventDates(
                new DateTime(2020, 2, 1),
                new DateTime(2020, 2, 2),
                new DateTime(2020, 1, 1),
                new DateTime(2020, 1, 2),
                _dateTime);
            var registrationRules = new EventRegistrationRules(10, 1, 1);
            var eventRegistrations = new EventRegistrations(1, eventDates, registrationRules, _dateTime);
            eventRegistrations.RegisterUser(1, "Test User", "TestUser@mail.com", ""); // register user with Id = 1
            eventRegistrations.RegisterUser(2, "Second Test User", "TestUserTwo@mail.com", ""); // register a second user
            Assert.Equal(2, eventRegistrations.Registrations.Count); // ensure registrations were added

            // Act
            eventRegistrations.StandbyRegistrationByUserId(1);

            // Assert
            Assert.Equal(1, eventRegistrations.CurrentStandbyCount);
        }
    }
}
