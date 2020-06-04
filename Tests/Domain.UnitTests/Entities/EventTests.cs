using EventsCore.Domain.Common;
using EventsCore.Domain.Entities;
using EventsCore.Domain.ValueObjects;
using EventsCore.Domain.UnitTests.Common;
using System;
using Xunit;
using EventsCore.Domain.Exceptions.Event;

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
            string newTitle = "";
            string newDescription = "new description";
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);

            EventDates eventDates = new EventDates(eventStart, eventEnd, regStart, regEnd, _dateTime);
            EventRegistrationRules regRules = new EventRegistrationRules(10);

            // Act/Assert
            Assert.Throws<EventArgumentException>(() => new Event(newTitle, newDescription, eventDates, regRules, 1));

        }

        [Fact]
        public void Should_Throw_EventArgumentException_For_Empty_Event_Description()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "";
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);

            EventDates eventDates = new EventDates(eventStart, eventEnd, regStart, regEnd, _dateTime);
            EventRegistrationRules regRules = new EventRegistrationRules(10);

            // Act/Assert
            Assert.Throws<EventArgumentException>(() => new Event(newTitle, newDescription, eventDates, regRules, 1));
        }
        [Fact]
        public void Should_Throw_EventArgumentException_For_EventTypeId_OutOfRange()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);

            EventDates eventDates = new EventDates(eventStart, eventEnd, regStart, regEnd, _dateTime);
            EventRegistrationRules regRules = new EventRegistrationRules(10);

            // Act/Assert
            Assert.Throws<EventArgumentException>(() => new Event(newTitle, newDescription, eventDates, regRules, 0));
        }
        [Fact]
        public void Should_Throw_EventArgumentException_For_EventDates_Null()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            EventDates eventDates = null;
            EventRegistrationRules regRules = new EventRegistrationRules(10);

            // Act/Assert
            Assert.Throws<EventArgumentException>(() => new Event(newTitle, newDescription, eventDates, regRules, 1));
        }
        [Fact]
        public void Should_Throw_EventArgumentException_For_RegistrationRules_Null()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);

            EventDates eventDates = new EventDates(eventStart, eventEnd, regStart, regEnd, _dateTime);
            EventRegistrationRules regRules = null;

            // Act/Assert
            Assert.Throws<EventArgumentException>(() => new Event(newTitle, newDescription, eventDates, regRules, 1));
        }
        [Fact]
        public void StartDate_Returns_Correct_Date()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);

            EventDates eventDates = new EventDates(eventStart, eventEnd, regStart, regEnd, _dateTime);
            EventRegistrationRules regRules = new EventRegistrationRules(10);
            var ev = new Event(newTitle, newDescription, eventDates, regRules, 1);
           
            // Act
            DateTime value = ev.StartDate;

            // Assert
            Assert.Equal(eventStart, value);
        }
        [Fact]
        public void EndDate_Returns_Correct_Date()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);

            EventDates eventDates = new EventDates(eventStart, eventEnd, regStart, regEnd, _dateTime);
            EventRegistrationRules regRules = new EventRegistrationRules(10);
            var ev = new Event(newTitle, newDescription, eventDates, regRules, 1);

            // Act
            DateTime value = ev.EndDate;

            // Assert
            Assert.Equal(eventEnd, value);
        }
        [Fact]
        public void RegistrationStartDate_Returns_Correct_Date()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);

            EventDates eventDates = new EventDates(eventStart, eventEnd, regStart, regEnd, _dateTime);
            EventRegistrationRules regRules = new EventRegistrationRules(10);
            var ev = new Event(newTitle, newDescription, eventDates, regRules, 1);

            // Act
            DateTime value = ev.RegistrationStartDate;

            // Assert
            Assert.Equal(regStart, value);
        }
        [Fact]
        public void RegistrationEndDate_Returns_Correct_Date()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);

            EventDates eventDates = new EventDates(eventStart, eventEnd, regStart, regEnd, _dateTime);
            EventRegistrationRules regRules = new EventRegistrationRules(10);
            var ev = new Event(newTitle, newDescription, eventDates, regRules, 1);

            // Act
            DateTime value = ev.RegistrationEndDate;

            // Assert
            Assert.Equal(regEnd, value);
        }
        [Fact]
        public void MaxRegistrations_Returns_Correct_Value()
        {        
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);

            EventDates eventDates = new EventDates(eventStart, eventEnd, regStart, regEnd, _dateTime);
            uint maxRegs = 10;
            EventRegistrationRules regRules = new EventRegistrationRules(maxRegs);
            var ev = new Event(newTitle, newDescription, eventDates, regRules, 1);

            // Act
            uint value = ev.MaxRegistrations;

            // Assert
            Assert.Equal(maxRegs, value);        
        }
        [Fact]
        public void Can_Update_Title()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);

            EventDates eventDates = new EventDates(eventStart, eventEnd, regStart, regEnd, _dateTime);            
            EventRegistrationRules regRules = new EventRegistrationRules(10);
            var ev = new Event(newTitle, newDescription, eventDates, regRules, 1);
            Assert.Equal(newTitle, ev.Title);

            // Act
            ev.UpdateTitle("Updated Title");

            // Assert
            Assert.Equal("Updated Title", ev.Title);
        }
        [Fact]
        public void Can_Update_Description()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);

            EventDates eventDates = new EventDates(eventStart, eventEnd, regStart, regEnd, _dateTime);
            EventRegistrationRules regRules = new EventRegistrationRules(10);
            var ev = new Event(newTitle, newDescription, eventDates, regRules, 1);
            Assert.Equal(newDescription, ev.Description);

            // Act
            ev.UpdateDescription("Updated Description");

            // Assert
            Assert.Equal("Updated Description", ev.Description);
        }
        [Fact]
        public void Can_Add_Event_To_EventSeries_By_Id()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);

            EventDates eventDates = new EventDates(eventStart, eventEnd, regStart, regEnd, _dateTime);
            EventRegistrationRules regRules = new EventRegistrationRules(10);
            var ev = new Event(newTitle, newDescription, eventDates, regRules, 1);
            Assert.Null(ev.EventSeriesId);

            // Act
            ev.AddEventToSeries(1);

            // Assert
            Assert.Equal(1, ev.EventSeriesId);
        }
        [Fact]
        public void Can_Remove_Event_From_EventSeries()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);

            EventDates eventDates = new EventDates(eventStart, eventEnd, regStart, regEnd, _dateTime);
            EventRegistrationRules regRules = new EventRegistrationRules(10);
            var ev = new Event(newTitle, newDescription, eventDates, regRules, 1, 1);
            Assert.Equal(1, ev.EventSeriesId);

            // Act
            ev.RemoveEventFromSeries();

            // Assert
            Assert.Null(ev.EventSeriesId);
        }
        [Fact]
        public void Can_Update_EventType_By_Id()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);

            EventDates eventDates = new EventDates(eventStart, eventEnd, regStart, regEnd, _dateTime);
            EventRegistrationRules regRules = new EventRegistrationRules(10);
            var ev = new Event(newTitle, newDescription, eventDates, regRules, 1);
            Assert.Equal(1, ev.EventTypeId);

            // Act
            ev.UpdateEventType(2);

            // Assert
            Assert.Equal(2, ev.EventTypeId);
        }
        [Fact]
        public void Can_Update_EventDates()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);

            EventDates eventDates = new EventDates(eventStart, eventEnd, regStart, regEnd, _dateTime);
            EventRegistrationRules regRules = new EventRegistrationRules(10);
            var ev = new Event(newTitle, newDescription, eventDates, regRules, 1);
            Assert.Equal(eventStart, ev.StartDate);
            Assert.Equal(eventEnd, ev.EndDate);
            Assert.Equal(regStart, ev.RegistrationStartDate);
            Assert.Equal(regEnd, ev.RegistrationEndDate);
            DateTime newStart = new DateTime(3000, 3, 1);
            DateTime newEnd = new DateTime(3000, 3, 2);
            DateTime newRegStart = new DateTime(3000, 2, 1);
            DateTime newRegEnd = new DateTime(3000, 2, 2);
            EventDates newDates = new EventDates(newStart,newEnd, newRegStart, newRegEnd, _dateTime);

            // Act
            ev.UpdateEventDates(newDates);

            // Assert
            Assert.Equal(newStart, ev.StartDate);
            Assert.Equal(newEnd, ev.EndDate);
            Assert.Equal(newRegStart, ev.RegistrationStartDate);
            Assert.Equal(newRegEnd, ev.RegistrationEndDate);
        }
        [Fact]
        public void Can_Update_EventRegistrationRules()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);

            EventDates eventDates = new EventDates(eventStart, eventEnd, regStart, regEnd, _dateTime);
            EventRegistrationRules regRules = new EventRegistrationRules(10);
            var ev = new Event(newTitle, newDescription, eventDates, regRules, 1);
            Assert.Equal(regRules, ev.Rules);
            EventRegistrationRules newRules = new EventRegistrationRules(10, 1, 5);
            // Act
            ev.UpdateRegistrationRules(newRules);

            // Assert
            Assert.Equal(newRules, ev.Rules);
        }
    }
}
