using EventsCore.Domain.Entities;
using EventsCore.Domain.Exceptions.EventSeries;
using Xunit;

namespace EventsCore.Domain.UnitTests.Entities
{
    public class EventSeriesTests
    {
        [Fact]
        public void Given_Valid_Values_EventSeries_Is_Valid()
        {
            // Arrange
            string title = "New Event Series";
            string description = "This is a new Event Series";

            // Act
            var es = new EventSeries(title, description);

            // Assert
            Assert.Equal("New Event Series", es.Title);
            Assert.Equal("This is a new Event Series", es.Description);
        }
        [Fact]
        public void Should_Throw_EventSeriesArgumentException_For_Empty_Title()
        {
            // Arrange
            string title = "";
            string description = "This is a new Event Series";

            // Act/Assert
            Assert.Throws<EventSeriesArgumentException>(() => new EventSeries(title, description));
        }
        [Fact]
        public void Should_Throw_EventSeriesArgumentException_For_Empty_Description()
        {
            // Arrange
            string title = "New Event Series";
            string description = "";

            // Act/Assert
            Assert.Throws<EventSeriesArgumentException>(() => new EventSeries(title, description));
        }
        [Fact]
        public void Can_Update_Title()
        {
            // Arrange
            string title = "New Event Series";
            string description = "This is a new Event Series";
            var es = new EventSeries(title, description);
            Assert.Equal("New Event Series", es.Title);

            // Act
            es.UpdateTitle("Updated Title");

            // Assert
            Assert.Equal("Updated Title", es.Title);
            
        }
        [Fact]
        public void Can_Update_Description()
        {
            // Arrange
            string title = "New Event Series";
            string description = "This is a new Event Series";
            var es = new EventSeries(title, description);
            Assert.Equal("This is a new Event Series", es.Description);

            // Act
            es.UpdateDescription("This is an updated description.");

            // Assert
            Assert.Equal("This is an updated description.", es.Description);

        }
    }
}
