using EventsCore.Domain.Common;
using EventsCore.Domain.Entities.EventRegistrationsAggregate;
using EventsCore.Domain.Entities.ValueObjects;
using EventsCore.Domain.Exceptions.EventRegistrationsAggregate;
using EventsCore.Domain.UnitTests.Common;
using System;
using System.Collections.Generic;
using System.Text;
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
            var evRegs = new EventRegistrations(eventId, EventDates, RegistrationRules);

            // Assert
            Assert.Equal(1, evRegs.EventId);
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
            Assert.Throws<EventRegistrationAggregateArgumentException>(() => new EventRegistrations(eventId, EventDates, RegistrationRules));
        }
        [Fact]
        public void Should_Throw_EventRegistrationAggregateArgumentException_For_Null_EventDates()
        {
            // Arrange
            int eventId = 1;
            EventDates eventDates = null;
            var registrationRules = new EventRegistrationRules(10);

            // Act/Assert
            Assert.Throws<EventRegistrationAggregateArgumentException>(() => new EventRegistrations(eventId, eventDates, registrationRules));
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
            Assert.Throws<EventRegistrationAggregateArgumentException>(() => new EventRegistrations(eventId, eventDates, registrationRules));
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
            var eventRegistrations = new EventRegistrations(eventId, eventDates, registrationRules);

            // Act
            var result = eventRegistrations.IsAcceptingRegistrations;

            // Assert
            Assert.False(result);            
        }
        [Fact]
        public void Given_Max_Registrations_IsAcceptionRegistrations_Returns_False()
        {

        }
    }
}
