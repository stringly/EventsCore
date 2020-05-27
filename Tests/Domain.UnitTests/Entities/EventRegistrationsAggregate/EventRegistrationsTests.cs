using EventsCore.Domain.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EventsCore.Domain.UnitTests.Entities.EventRegistrationsAggregate
{
    public class EventRegistrationsTests
    {
        [Fact]
        public void EventRegistrations_Given_Valid_Values_Is_Valid()
        {
            // Arrange
            int eventId = 1;
            var EventDates = new EventDates(
                new DateTime(3000, 2, 1),
                new DateTime(3000, 2, 2),
                new DateTime(3000, 1, 1),
                new DateTime(3000, 1, 2));
            var RegistrationRules = new EventRegistrationRules(10);

            // Act

            // Assert
        }
    }
}
