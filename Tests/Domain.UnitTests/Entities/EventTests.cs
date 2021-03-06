﻿using EventsCore.Common;
using EventsCore.Domain.Entities;
using EventsCore.Domain.Exceptions.Event;
using EventsCore.Domain.UnitTests.Common;
using EventsCore.Domain.ValueObjects;
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
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;
            
            EventDates eventDates = new EventDates(eventStart, eventEnd, regStart, regEnd); // value object for assertion
            EventRegistrationRules regRules = new EventRegistrationRules(10); // value object for assertion

            // Act
            Event e = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs);

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
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;
            // Act/Assert
            Assert.Throws<EventArgumentException>(() => new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs));

        }

        [Fact]
        public void Should_Throw_EventArgumentException_For_Empty_Event_Description()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "";
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;

            // Act/Assert
            Assert.Throws<EventArgumentException>(() => new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs));
        }
        [Fact]
        public void Should_Throw_EventArgumentException_For_EventTypeId_OutOfRange()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            int inValidEventTypeId = 0;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;

            // Act/Assert
            Assert.Throws<EventArgumentException>(() => new Event(newTitle, newDescription, inValidEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs));
        }
        [Fact]
        public void StartDate_Returns_Correct_Date()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;
            var ev = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs);
           
            // Act
            DateTime value = ev.Dates.StartDate;

            // Assert
            Assert.Equal(eventStart, value);
        }
        [Fact]
        public void EndDate_Returns_Correct_Date()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;
            var ev = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs);

            // Act
            DateTime value = ev.Dates.EndDate;

            // Assert
            Assert.Equal(eventEnd, value);
        }
        [Fact]
        public void RegistrationStartDate_Returns_Correct_Date()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;
            var ev = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs);

            // Act
            DateTime value = ev.Dates.RegistrationStartDate;

            // Assert
            Assert.Equal(regStart, value);
        }
        [Fact]
        public void RegistrationEndDate_Returns_Correct_Date()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;
            var ev = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs);

            // Act
            DateTime value = ev.Dates.RegistrationEndDate;

            // Assert
            Assert.Equal(regEnd, value);
        }
        [Fact]
        public void MaxRegistrations_Returns_Correct_Value()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;
            var ev = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs);

            // Act
            uint value = ev.Rules.MaxRegistrations;

            // Assert
            Assert.Equal(maxRegs, (int)value);        
        }
        [Fact]
        public void Can_Update_Title()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;
            var ev = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs);
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
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;
            var ev = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs);
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
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;
            int validEventSeriesId = 1;
            var ev = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs);
            Assert.Null(ev.EventSeriesId);

            // Act
            ev.AddEventToSeries(validEventSeriesId);

            // Assert
            Assert.Equal(validEventSeriesId, ev.EventSeriesId);
        }
        [Fact]
        public void Can_Remove_Event_From_EventSeries()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;
            int validEventSeriesId = 1;
            var ev = new Event(newTitle, newDescription, validEventTypeId, validEventSeriesId, eventStart, eventEnd, regStart, regEnd, maxRegs);
            Assert.Equal(validEventSeriesId, ev.EventSeriesId);

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
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;
            int validUpdatedEventTypeId = 2;
            var ev = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs);
            Assert.Equal(1, ev.EventTypeId);

            // Act
            ev.UpdateEventType(validUpdatedEventTypeId);

            // Assert
            Assert.Equal(validUpdatedEventTypeId, ev.EventTypeId);
        }
        [Fact]
        public void Can_Update_EventDates()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;
            var ev = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs);

            Assert.Equal(eventStart, ev.Dates.StartDate);
            Assert.Equal(eventEnd, ev.Dates.EndDate);
            Assert.Equal(regStart, ev.Dates.RegistrationStartDate);
            Assert.Equal(regEnd, ev.Dates.RegistrationEndDate);

            DateTime newStart = new DateTime(3000, 3, 1);
            DateTime newEnd = new DateTime(3000, 3, 2);
            DateTime newRegStart = new DateTime(3000, 2, 1);
            DateTime newRegEnd = new DateTime(3000, 2, 2);
            

            // Act
            ev.UpdateEventDates(newStart, newEnd, newRegStart, newRegEnd);

            // Assert
            Assert.Equal(newStart, ev.Dates.StartDate);
            Assert.Equal(newEnd, ev.Dates.EndDate);
            Assert.Equal(newRegStart, ev.Dates.RegistrationStartDate);
            Assert.Equal(newRegEnd, ev.Dates.RegistrationEndDate);
        }
        [Fact]
        public void Can_Update_StartDate()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;
            var ev = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs);

            Assert.Equal(eventStart, ev.Dates.StartDate);
            Assert.Equal(eventEnd, ev.Dates.EndDate);
            Assert.Equal(regStart, ev.Dates.RegistrationStartDate);
            Assert.Equal(regEnd, ev.Dates.RegistrationEndDate);

            DateTime validNewStartDate = new DateTime(3000, 1, 30);

            // Act
            ev.UpdateEventDates(validNewStartDate);

            // Assert
            Assert.Equal(validNewStartDate, ev.Dates.StartDate);
            Assert.Equal(eventEnd, ev.Dates.EndDate);
            Assert.Equal(regStart, ev.Dates.RegistrationStartDate);
            Assert.Equal(regEnd, ev.Dates.RegistrationEndDate);
        }
        [Fact]
        public void Can_Update_EndDate()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;
            var ev = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs);

            Assert.Equal(eventStart, ev.Dates.StartDate);
            Assert.Equal(eventEnd, ev.Dates.EndDate);
            Assert.Equal(regStart, ev.Dates.RegistrationStartDate);
            Assert.Equal(regEnd, ev.Dates.RegistrationEndDate);

            DateTime validNewEndDate = new DateTime(3000, 2, 3);

            // Act
            ev.UpdateEventDates(null, validNewEndDate);

            // Assert
            Assert.Equal(eventStart, ev.Dates.StartDate);
            Assert.Equal(validNewEndDate, ev.Dates.EndDate);
            Assert.Equal(regStart, ev.Dates.RegistrationStartDate);
            Assert.Equal(regEnd, ev.Dates.RegistrationEndDate);
        }
        [Fact]
        public void Can_Update_RegistrationStartDate()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 3);
            int maxRegs = 10;
            var ev = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs);

            Assert.Equal(eventStart, ev.Dates.StartDate);
            Assert.Equal(eventEnd, ev.Dates.EndDate);
            Assert.Equal(regStart, ev.Dates.RegistrationStartDate);
            Assert.Equal(regEnd, ev.Dates.RegistrationEndDate);

            DateTime validNewRegStartDate = new DateTime(3000, 1, 2);

            // Act
            ev.UpdateEventDates(null, null, validNewRegStartDate);

            // Assert
            Assert.Equal(eventStart, ev.Dates.StartDate);
            Assert.Equal(eventEnd, ev.Dates.EndDate);
            Assert.Equal(validNewRegStartDate, ev.Dates.RegistrationStartDate);
            Assert.Equal(regEnd, ev.Dates.RegistrationEndDate);
        }
        [Fact]
        public void Can_Update_RegistrationEndDate()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 3);
            int maxRegs = 10;
            var ev = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs);

            Assert.Equal(eventStart, ev.Dates.StartDate);
            Assert.Equal(eventEnd, ev.Dates.EndDate);
            Assert.Equal(regStart, ev.Dates.RegistrationStartDate);
            Assert.Equal(regEnd, ev.Dates.RegistrationEndDate);

            DateTime validNewRegEndDate = new DateTime(3000, 1, 2);

            // Act
            ev.UpdateEventDates(null, null, null, validNewRegEndDate);

            // Assert
            Assert.Equal(eventStart, ev.Dates.StartDate);
            Assert.Equal(eventEnd, ev.Dates.EndDate);
            Assert.Equal(regStart, ev.Dates.RegistrationStartDate);
            Assert.Equal(validNewRegEndDate, ev.Dates.RegistrationEndDate);
        }
        [Fact]
        public void Can_Update_EventRegistrationRules()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;

            EventRegistrationRules regRules = new EventRegistrationRules((uint)maxRegs); // value object for assertion

            var ev = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs);

            Assert.Equal(regRules, ev.Rules);

            EventRegistrationRules newRules = new EventRegistrationRules(10, 1, 5);
            // Act
            ev.UpdateRegistrationRules(maxRegs, 1, 5);

            // Assert
            Assert.Equal(newRules, ev.Rules);
        }
        [Fact]
        public void Can_Update_MaxRegistrations()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;

            var ev = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs);

            Assert.Equal(maxRegs, (int)ev.Rules.MaxRegistrations);
            int newValidMaxRegs = 20;
            // Act
            ev.UpdateRegistrationRules(20);

            // Assert
            Assert.Equal(newValidMaxRegs, (int)ev.Rules.MaxRegistrations);

        }
        [Fact]
        public void Can_Update_MinRegistrations() 
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;
            int minRegs = 2;

            var ev = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs, minRegs);
            Assert.Equal(minRegs, (int)ev.Rules.MinRegistrations);
            int newValidMinRegs = 9;
            // Act
            ev.UpdateRegistrationRules(null, newValidMinRegs);

            // Assert
            Assert.Equal(maxRegs, (int)ev.Rules.MaxRegistrations);
            Assert.Equal(newValidMinRegs, (int)ev.Rules.MinRegistrations);
        }
        [Fact]
        public void Can_Update_StandbyRegs()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;
            int minRegs = 2;
            int maxStandbyRegs = 20;
            var ev = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs, minRegs, maxStandbyRegs);
            Assert.Equal(maxRegs, (int)ev.Rules.MaxRegistrations);
            Assert.Equal(minRegs, (int)ev.Rules.MinRegistrations);
            Assert.Equal(maxStandbyRegs, (int)ev.Rules.MaxStandbyRegistrations);
            int newValidStandbyRegs = 10;
            // Act
            ev.UpdateRegistrationRules(null, null, newValidStandbyRegs);

            // Assert
            Assert.Equal(maxRegs, (int)ev.Rules.MaxRegistrations);
            Assert.Equal(minRegs, (int)ev.Rules.MinRegistrations);
            Assert.Equal(newValidStandbyRegs, (int)ev.Rules.MaxStandbyRegistrations);
        }
        [Fact]
        public void Can_Update_MinAndMaxRegs()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;
            int minRegs = 2;
            int maxStandbyRegs = 20;
            var ev = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs, minRegs, maxStandbyRegs);
            Assert.Equal(maxRegs, (int)ev.Rules.MaxRegistrations);
            Assert.Equal(minRegs, (int)ev.Rules.MinRegistrations);
            Assert.Equal(maxStandbyRegs, (int)ev.Rules.MaxStandbyRegistrations);
            int newValidMaxRegs = 10;
            int newValidMinRegs = 5;
            // Act
            ev.UpdateRegistrationRules(newValidMaxRegs, newValidMinRegs);

            // Assert
            Assert.Equal(newValidMaxRegs, (int)ev.Rules.MaxRegistrations);
            Assert.Equal(newValidMinRegs, (int)ev.Rules.MinRegistrations);
            Assert.Equal(maxStandbyRegs, (int)ev.Rules.MaxStandbyRegistrations);
        }
        [Fact]
        public void Can_Update_MaxAndStandbyRegs()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;
            int minRegs = 2;
            int maxStandbyRegs = 20;
            var ev = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs, minRegs, maxStandbyRegs);
            Assert.Equal(maxRegs, (int)ev.Rules.MaxRegistrations);
            Assert.Equal(minRegs, (int)ev.Rules.MinRegistrations);
            Assert.Equal(maxStandbyRegs, (int)ev.Rules.MaxStandbyRegistrations);
            int newValidMaxRegs = 10;
            int newValidStandbyRegs = 5;

            // Act
            ev.UpdateRegistrationRules(newValidMaxRegs, null, newValidStandbyRegs);

            // Assert
            Assert.Equal(newValidMaxRegs, (int)ev.Rules.MaxRegistrations);
            Assert.Equal(minRegs, (int)ev.Rules.MinRegistrations);
            Assert.Equal(newValidStandbyRegs, (int)ev.Rules.MaxStandbyRegistrations);
        }
        [Fact]
        public void Can_Update_MinAndStandbyRegs()
        {
            // Arrange
            string newTitle = "Event Title";
            string newDescription = "new description";
            int validEventTypeId = 1;
            DateTime eventStart = new DateTime(3000, 2, 1);
            DateTime eventEnd = new DateTime(3000, 2, 2);
            DateTime regStart = new DateTime(3000, 1, 1);
            DateTime regEnd = new DateTime(3000, 1, 2);
            int maxRegs = 10;
            int minRegs = 2;
            int maxStandbyRegs = 20;
            var ev = new Event(newTitle, newDescription, validEventTypeId, eventStart, eventEnd, regStart, regEnd, maxRegs, minRegs, maxStandbyRegs);
            Assert.Equal(maxRegs, (int)ev.Rules.MaxRegistrations);
            Assert.Equal(minRegs, (int)ev.Rules.MinRegistrations);
            Assert.Equal(maxStandbyRegs, (int)ev.Rules.MaxStandbyRegistrations);
            int newValidMinRegs = 5;
            int newValidStandbyRegs = 5;
            // Act
            ev.UpdateRegistrationRules(null, newValidMinRegs, newValidStandbyRegs);

            // Assert
            Assert.Equal(maxRegs, (int)ev.Rules.MaxRegistrations);
            Assert.Equal(newValidMinRegs, (int)ev.Rules.MinRegistrations);
            Assert.Equal(newValidStandbyRegs, (int)ev.Rules.MaxStandbyRegistrations);
        }
        [Fact]
        public void Can_Create_Event_With_Address()
        {
            // Arrange
            string validTitle = "Valid Event Title";
            string validDescription = "Valid event description";
            int validEventTypeId = 1;
            DateTime validStartDate = new DateTime(3000, 2, 1);
            DateTime validEndDate = new DateTime(3000, 2, 2);
            DateTime validRegStart = new DateTime(3000, 1, 1);
            DateTime validRegEnd = new DateTime(3000, 1, 2);
            int validMaxRegs = 10;
            string validStreet = "123 Anywhere St.";
            string validSuite = "Room #123";
            string validCity = "Yourtown";
            string validState = "MD";
            string validZip = "12345";

            // Act
            var e = new Event(
                validTitle,
                validDescription,
                validEventTypeId,
                validStartDate,
                validEndDate,
                validRegStart,
                validRegEnd,
                validMaxRegs,
                validStreet,
                validSuite,
                validCity,
                validState,
                validZip
                );

            // Assert
            Assert.Equal(validTitle, e.Title);
            Assert.Equal(validDescription, e.Description);
            Assert.Equal(validEventTypeId, e.EventTypeId);
            Assert.Null(e.EventSeriesId);
            Assert.Equal(validStreet, e.Address.Street);
            Assert.Equal(validSuite, e.Address.Suite);
            Assert.Equal(validCity, e.Address.City);
            Assert.Equal(validState, e.Address.State);
            Assert.Equal(validZip, e.Address.ZipCode);
        }
        [Theory]
        [InlineData("Room 123")]
        [InlineData("")]
        public void Location_Returns_Correct_String(string value)
        {
            // Arrange
            string validTitle = "Valid Event Title";
            string validDescription = "Valid event description";
            int validEventTypeId = 1;
            DateTime validStartDate = new DateTime(3000, 2, 1);
            DateTime validEndDate = new DateTime(3000, 2, 2);
            DateTime validRegStart = new DateTime(3000, 1, 1);
            DateTime validRegEnd = new DateTime(3000, 1, 2);
            int validMaxRegs = 10;
            string validStreet = "123 Anywhere St.";
            string validSuite = value;
            string validCity = "Yourtown";
            string validState = "MD";
            string validZip = "12345";
            var e = new Event(
                validTitle,
                validDescription,
                validEventTypeId,
                validStartDate,
                validEndDate,
                validRegStart,
                validRegEnd,
                validMaxRegs,
                validStreet,
                validSuite,
                validCity,
                validState,
                validZip
                );
            string expected = $"{validStreet} {validSuite} {validCity}, {validState} {validZip}";

            // Act
            string actual = e.Location;
            // Assert
            Assert.Equal(expected, actual);

        }
        [Fact]
        public void Can_Update_Address_Street()
        {
            // Arrange
            string validTitle = "Valid Event Title";
            string validDescription = "Valid event description";
            int validEventTypeId = 1;
            DateTime validStartDate = new DateTime(3000, 2, 1);
            DateTime validEndDate = new DateTime(3000, 2, 2);
            DateTime validRegStart = new DateTime(3000, 1, 1);
            DateTime validRegEnd = new DateTime(3000, 1, 2);
            int validMaxRegs = 10;
            string validStreet = "123 Anywhere St.";
            string validSuite = "Room #123";
            string validCity = "Yourtown";
            string validState = "MD";
            string validZip = "12345";
            var e = new Event(
                validTitle,
                validDescription,
                validEventTypeId,
                validStartDate,
                validEndDate,
                validRegStart,
                validRegEnd,
                validMaxRegs,
                validStreet,
                validSuite,
                validCity,
                validState,
                validZip
                );
            Assert.Equal(validStreet, e.Address.Street);
            string newValidStreet = "999 New Street";
            // Act
            e.UpdateAddress(newValidStreet);

            // Assert
            Assert.Equal(newValidStreet, e.Address.Street);
            Assert.Equal(validSuite, e.Address.Suite);
            Assert.Equal(validCity, e.Address.City);
            Assert.Equal(validState, e.Address.State);
            Assert.Equal(validZip, e.Address.ZipCode);

        }
        [Fact]
        public void Can_Update_Address_Suite()
        {
            // Arrange
            string validTitle = "Valid Event Title";
            string validDescription = "Valid event description";
            int validEventTypeId = 1;
            DateTime validStartDate = new DateTime(3000, 2, 1);
            DateTime validEndDate = new DateTime(3000, 2, 2);
            DateTime validRegStart = new DateTime(3000, 1, 1);
            DateTime validRegEnd = new DateTime(3000, 1, 2);
            int validMaxRegs = 10;
            string validStreet = "123 Anywhere St.";
            string validSuite = "Room #123";
            string validCity = "Yourtown";
            string validState = "MD";
            string validZip = "12345";
            var e = new Event(
                validTitle,
                validDescription,
                validEventTypeId,
                validStartDate,
                validEndDate,
                validRegStart,
                validRegEnd,
                validMaxRegs,
                validStreet,
                validSuite,
                validCity,
                validState,
                validZip
                );
            Assert.Equal(validStreet, e.Address.Street);
            string newValidSuite = "Conference room 2";

            // Act
            e.UpdateAddress("", newValidSuite);

            // Assert
            Assert.Equal(validStreet, e.Address.Street);
            Assert.Equal(newValidSuite, e.Address.Suite);
            Assert.Equal(validCity, e.Address.City);
            Assert.Equal(validState, e.Address.State);
            Assert.Equal(validZip, e.Address.ZipCode);
        }
        [Fact]
        public void Can_Update_City()
        {
            // Arrange
            string validTitle = "Valid Event Title";
            string validDescription = "Valid event description";
            int validEventTypeId = 1;
            DateTime validStartDate = new DateTime(3000, 2, 1);
            DateTime validEndDate = new DateTime(3000, 2, 2);
            DateTime validRegStart = new DateTime(3000, 1, 1);
            DateTime validRegEnd = new DateTime(3000, 1, 2);
            int validMaxRegs = 10;
            string validStreet = "123 Anywhere St.";
            string validSuite = "Room #123";
            string validCity = "Yourtown";
            string validState = "MD";
            string validZip = "12345";
            var e = new Event(
                validTitle,
                validDescription,
                validEventTypeId,
                validStartDate,
                validEndDate,
                validRegStart,
                validRegEnd,
                validMaxRegs,
                validStreet,
                validSuite,
                validCity,
                validState,
                validZip
                );
            Assert.Equal(validStreet, e.Address.Street);
            string newValidCity = "New Jack City";

            // Act
            e.UpdateAddress("", "", newValidCity);

            // Assert
            Assert.Equal(validStreet, e.Address.Street);
            Assert.Equal(validSuite, e.Address.Suite);
            Assert.Equal(newValidCity, e.Address.City);
            Assert.Equal(validState, e.Address.State);
            Assert.Equal(validZip, e.Address.ZipCode);
        }
        [Fact]
        public void Can_Update_State()
        {
            // Arrange
            string validTitle = "Valid Event Title";
            string validDescription = "Valid event description";
            int validEventTypeId = 1;
            DateTime validStartDate = new DateTime(3000, 2, 1);
            DateTime validEndDate = new DateTime(3000, 2, 2);
            DateTime validRegStart = new DateTime(3000, 1, 1);
            DateTime validRegEnd = new DateTime(3000, 1, 2);
            int validMaxRegs = 10;
            string validStreet = "123 Anywhere St.";
            string validSuite = "Room #123";
            string validCity = "Yourtown";
            string validState = "MD";
            string validZip = "12345";
            var e = new Event(
                validTitle,
                validDescription,
                validEventTypeId,
                validStartDate,
                validEndDate,
                validRegStart,
                validRegEnd,
                validMaxRegs,
                validStreet,
                validSuite,
                validCity,
                validState,
                validZip
                );
            Assert.Equal(validStreet, e.Address.Street);
            string newValidState = "AK";

            // Act
            e.UpdateAddress("", "", "", newValidState);

            // Assert
            Assert.Equal(validStreet, e.Address.Street);
            Assert.Equal(validSuite, e.Address.Suite);
            Assert.Equal(validCity, e.Address.City);
            Assert.Equal(newValidState, e.Address.State);
            Assert.Equal(validZip, e.Address.ZipCode);
        }
        [Fact]
        public void Can_Update_Zip()
        {
            // Arrange
            string validTitle = "Valid Event Title";
            string validDescription = "Valid event description";
            int validEventTypeId = 1;
            DateTime validStartDate = new DateTime(3000, 2, 1);
            DateTime validEndDate = new DateTime(3000, 2, 2);
            DateTime validRegStart = new DateTime(3000, 1, 1);
            DateTime validRegEnd = new DateTime(3000, 1, 2);
            int validMaxRegs = 10;
            string validStreet = "123 Anywhere St.";
            string validSuite = "Room #123";
            string validCity = "Yourtown";
            string validState = "MD";
            string validZip = "12345";
            var e = new Event(
                validTitle,
                validDescription,
                validEventTypeId,
                validStartDate,
                validEndDate,
                validRegStart,
                validRegEnd,
                validMaxRegs,
                validStreet,
                validSuite,
                validCity,
                validState,
                validZip
                );
            Assert.Equal(validStreet, e.Address.Street);
            string newValidZip = "99999";

            // Act
            e.UpdateAddress("", "", "", "", newValidZip);

            // Assert
            Assert.Equal(validStreet, e.Address.Street);
            Assert.Equal(validSuite, e.Address.Suite);
            Assert.Equal(validCity, e.Address.City);
            Assert.Equal(validState, e.Address.State);
            Assert.Equal(newValidZip, e.Address.ZipCode);
        }
        [Theory]
        [InlineData("")]
        [InlineData("           ")]
        public void Given_Empty_StreetAddress_Should_Throw_EventArgumentException(string value)
        {
            // Arrange
            string validTitle = "Valid Event Title";
            string validDescription = "Valid event description";
            int validEventTypeId = 1;
            DateTime validStartDate = new DateTime(3000, 2, 1);
            DateTime validEndDate = new DateTime(3000, 2, 2);
            DateTime validRegStart = new DateTime(3000, 1, 1);
            DateTime validRegEnd = new DateTime(3000, 1, 2);
            int validMaxRegs = 10;
            string inValidStreet = value;
            string validSuite = "Room #123";
            string validCity = "Yourtown";
            string validState = "MD";
            string validZip = "12345";

            // Act/Assert
            Assert.Throws<EventArgumentException>(() => new Event(
                validTitle,
                validDescription,
                validEventTypeId,
                validStartDate,
                validEndDate,
                validRegStart,
                validRegEnd,
                validMaxRegs,
                inValidStreet,
                validSuite,
                validCity,
                validState,
                validZip
                ));
        }
        [Theory]
        [InlineData("")]
        [InlineData("           ")]
        public void Given_Empty_City_Should_Throw_EventArgumentException(string value)
        {
            // Arrange
            string validTitle = "Valid Event Title";
            string validDescription = "Valid event description";
            int validEventTypeId = 1;
            DateTime validStartDate = new DateTime(3000, 2, 1);
            DateTime validEndDate = new DateTime(3000, 2, 2);
            DateTime validRegStart = new DateTime(3000, 1, 1);
            DateTime validRegEnd = new DateTime(3000, 1, 2);
            int validMaxRegs = 10;
            string validStreet = "123 Anywhere St.";
            string validSuite = "Room #123";
            string inValidCity = value;
            string validState = "MD";
            string validZip = "12345";

            // Act/Assert
            Assert.Throws<EventArgumentException>(() => new Event(
                validTitle,
                validDescription,
                validEventTypeId,
                validStartDate,
                validEndDate,
                validRegStart,
                validRegEnd,
                validMaxRegs,
                validStreet,
                validSuite,
                inValidCity,
                validState,
                validZip
                ));
        }
        [Theory]
        [InlineData("")]
        [InlineData("           ")]
        public void Given_Empty_State_Should_Throw_EventArgumentException(string value)
        {
            // Arrange
            string validTitle = "Valid Event Title";
            string validDescription = "Valid event description";
            int validEventTypeId = 1;
            DateTime validStartDate = new DateTime(3000, 2, 1);
            DateTime validEndDate = new DateTime(3000, 2, 2);
            DateTime validRegStart = new DateTime(3000, 1, 1);
            DateTime validRegEnd = new DateTime(3000, 1, 2);
            int validMaxRegs = 10;
            string validStreet = "123 Anywhere St.";
            string validSuite = "Room #123";
            string validCity = "Yourtown";
            string inValidState = value;
            string validZip = "12345";

            // Act/Assert
            Assert.Throws<EventArgumentException>(() => new Event(
                validTitle,
                validDescription,
                validEventTypeId,
                validStartDate,
                validEndDate,
                validRegStart,
                validRegEnd,
                validMaxRegs,
                validStreet,
                validSuite,
                validCity,
                inValidState,
                validZip
                ));
        }
        [Theory]
        [InlineData("")]
        [InlineData("           ")]
        public void Given_Empty_Zip_Should_Throw_EventArgumentException(string value)
        {
            // Arrange
            string validTitle = "Valid Event Title";
            string validDescription = "Valid event description";
            int validEventTypeId = 1;
            DateTime validStartDate = new DateTime(3000, 2, 1);
            DateTime validEndDate = new DateTime(3000, 2, 2);
            DateTime validRegStart = new DateTime(3000, 1, 1);
            DateTime validRegEnd = new DateTime(3000, 1, 2);
            int validMaxRegs = 10;
            string validStreet = "123 Anywhere St.";
            string validSuite = "Room #123";
            string validCity = "Yourtown";
            string validState = "MD";
            string inValidZip = value;

            // Act/Assert
            Assert.Throws<EventArgumentException>(() => new Event(
                validTitle,
                validDescription,
                validEventTypeId,
                validStartDate,
                validEndDate,
                validRegStart,
                validRegEnd,
                validMaxRegs,
                validStreet,
                validSuite,
                validCity,
                validState,
                inValidZip
                ));
        }
    }
}
