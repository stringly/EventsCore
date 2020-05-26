using EventsCore.Domain.Entities.EventAggregate;
using EventsCore.Domain.Exceptions.EventAggregate;
using System;
using Xunit;

namespace EventsCore.Domain.UnitTests.ValueObjects
{
    public class EventDatesTests
    {
        [Fact]
        public void EventDates_Given_Valid_Dates_Is_Valid()
        {
            // Arrange
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 15);

            // Act
            var eventDates = new EventDates(eventStart, eventEnd, regStart, regEnd);

            // Assert
            Assert.Equal(eventDates.StartDate, eventStart);
            Assert.Equal(eventDates.EndDate, eventEnd);
            Assert.Equal(eventDates.RegistrationStartDate, regStart);
            Assert.Equal(eventDates.RegistrationEndDate, regEnd);            
        }

        [Fact]
        public void Should_Throw_EventDatesInvalidException_For_StartDate_In_Past()
        {
            // Arrange
            DateTime eventStart = new DateTime(2020, 2, 1);
            DateTime eventEnd = new DateTime(2020, 2, 2);
            DateTime regStart = new DateTime(2020, 1, 1);
            DateTime regEnd = new DateTime(2020, 1, 15);

            // Act/Assert
            Assert.Throws<EventDatesInvalidException>(() => new EventDates(eventStart, eventEnd, regStart, regEnd));
        }

        [Fact]
        public void Should_Throw_EventDatesInvalidException_For_StartDate_After_EndDate()
        {
            // Arrange
            DateTime eventStart = new DateTime(3000, 2, 2);
            DateTime eventEnd = new DateTime(3000, 2, 1);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 15);

            // Act/Assert
            Assert.Throws<EventDatesInvalidException>(() => new EventDates(eventStart, eventEnd, regStart, regEnd));
        }
        [Fact]
        public void Should_Throw_EventDatesInvalidException_For_RegistrationStart_After_RegistrationEnd()
        {
            // Arrange
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 2);
            DateTime regEnd = new DateTime(3000, 1, 1);

            // Act/Assert
            Assert.Throws<EventDatesInvalidException>(() => new EventDates(eventStart, eventEnd, regStart, regEnd));
        }
        [Fact]
        public void Should_Throw_EventDatesInvalidException_ForRegistrationStart_After_EventEnd()
        {
            // Arrange
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 2, 3);
            DateTime regEnd = new DateTime(3000, 2, 4);

            // Act/Assert
            Assert.Throws<EventDatesInvalidException>(() => new EventDates(eventStart, eventEnd, regStart, regEnd));
        }
    }
}
