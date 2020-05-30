using EventsCore.Domain.Common;
using EventsCore.Domain.Entities;
using EventsCore.Domain.Entities.ValueObjects;
using EventsCore.Domain.UnitTests.Common;
using System;
using Xunit;

namespace EventsCore.Domain.UnitTests.Entities
{
    public class EventTests
    {
        private readonly IDateTime _dateTime;
        public EventTests()
        {
            _dateTime = new DateTimeTestProvider();
        }
        [Fact]
        public void Event_Given_Valid_Values_Is_Valid()
        {
            // Arrange
            string newTitle = "new title";
            string newDescription = "new description";
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);

            EventDates eventDates = new EventDates(eventStart, eventEnd, regStart, regEnd, _dateTime);
            EventRegistrationRules regRules = new EventRegistrationRules(10);

            // Act
            Event e = new Event(newTitle, newDescription, eventDates, regRules, 1);


            // Assert
            Assert.Equal("new title", e.Title);
            Assert.Equal("new description",e.Description);
            Assert.Equal(eventDates, e.Dates);
            Assert.Equal(regRules, e.Rules);
            Assert.Equal(1, e.EventTypeId);
            Assert.Null(e.EventSeriesId);
        }
        [Fact]
        public void Should_Throw_EventArgumentException_For_Empty_Event_Title()
        {
            // Arrange


            // Act/Assert
            //Assert.Throws<EventDatesInvalidException>(() => new Event(/* add parameters here */));

        }

        [Fact]
        public void Should_Throw_EventArgumentException_For_Empty_Event_Description()
        {
            // Arrange




            // Act/Assert
            //Assert.Throws<EventDatesInvalidException>(() => new Event(/* add parameters here */));
        }
    }
}
