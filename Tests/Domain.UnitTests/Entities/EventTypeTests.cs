using EventsCore.Domain.Entities;
using EventsCore.Domain.Exceptions.EventType;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EventsCore.Domain.UnitTests.Entities
{
    public class EventTypeTests
    {
        [Fact]
        public void Given_Valid_Values_EventType_Is_Valid()
        {
            // Arrange
            string name = "New EventType";

            // Act
            var et = new EventType(name);

            // Assert
            Assert.Equal("New EventType", et.Name);
        }
        [Fact]
        public void Should_Throw_EventTypeArgumentException_For_Empty_Name()
        {
            // Arrange
            string name = "";

            // Act/Assert
            Assert.Throws<EventTypeArgumentException>(() => new EventType(name));
        }
        [Fact]
        public void Can_Update_Name()
        {
            // Arrange
            string name = "New Event Type";
            var et = new EventType(name);
            Assert.Equal(name, et.Name);
            // Act
            et.UpdateName("Updated Name");
            // Assert
            Assert.Equal("Updated Name", et.Name);
        }
    }
}
