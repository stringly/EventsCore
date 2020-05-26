using EventsCore.Domain.Entities.EventAggregate;
using EventsCore.Domain.Exceptions.EventAggregate;
using System;
using Xunit;

namespace EventsCore.Domain.UnitTests.Entities.EventAggregate
{
    public class EventTests
    {
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

            EventDates eventDates = new EventDates(eventStart, eventEnd, regStart, regEnd);
            EventRegistrationRules regRules = new EventRegistrationRules(10);

            // Act
            Event e = new Event(newTitle, newDescription, eventDates, regRules);


            // Assert
            Assert.Equal("new title", e.Title);
            Assert.Equal("new description",e.Description);
            Assert.Equal(eventDates, e.Dates);
            Assert.Equal(regRules, e.Rules);
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
