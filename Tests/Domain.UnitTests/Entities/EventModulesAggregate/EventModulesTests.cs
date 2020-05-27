using EventsCore.Domain.Entities.EventModulesAggregate;
using EventsCore.Domain.Exceptions.EventModulesAggregate;
using System.Linq;
using Xunit;

namespace EventsCore.Domain.UnitTests.Entities.EventModulesAggregate
{
    public class EventModulesTests
    {
        [Fact]
        public void EventModule_Given_Valid_Values_Is_Valid()
        {
            // Arrange
            int fakeEventId = 1;

            // Act
            var eventModules = new EventModules(fakeEventId);

            // Assert
            Assert.Equal(1, eventModules.EventId);
            Assert.NotNull(eventModules.Modules);
        }
        [Fact]
        public void Should_Throw_EventModulesAggregateArgumentException_For_EventID_Out_Of_Range()
        {
            // Arrange
            int fakeEventId = 0; // as of now, the only range enforced is non-zero

            // Act/Assert
            Assert.Throws<EventModulesAggregateArgumentException>(() => new EventModules(fakeEventId));
        }
        [Fact]
        public void Can_Add_Module_To_Empty_Modules_List()
        {
            // Arrange
            var eventModules = new EventModules(1);
            // Act            
            eventModules.AddModule("Test Module", "This is a test module.");

            // Assert
            Assert.NotEmpty(eventModules.Modules);
            var addedModule = eventModules.Modules.First();
            Assert.Equal("Test Module", addedModule.ModuleName);
            Assert.Equal("This is a test module.", addedModule.Description);
        }
        [Fact]
        public void Can_Remove_Module_From_Modules_By_Id()
        {
            // Arrange
            var eventModules = new EventModules(1);            
            eventModules.AddModule("Test Module", "This is a test module.");
            Assert.NotEmpty(eventModules.Modules);

            // Act
            eventModules.RemoveModuleById(0);

            // Assert
            Assert.Empty(eventModules.Modules);
            
        }
        [Fact]
        public void Should_Throw_EventModulesAggregateArgumentException_If_Removing_Module_Id_Does_Not_Exist()
        {
            // Arrange
            var eventModules = new EventModules(1);
            eventModules.AddModule("Test Module", "This is a test module.");
            Assert.NotEmpty(eventModules.Modules);

            // Act/Assert
            Assert.Throws<EventModulesAggregateArgumentException>(() => eventModules.RemoveModuleById(1));
        }

        [Fact]
        public void Should_Throw_EventModulesAggregateArgumentException_Id_Adding_Module_If_Module_Name_Taken() 
        {
            // Arrange
            var eventModules = new EventModules(1);
            eventModules.AddModule("Test Module", "This is a test module.");
            Assert.NotEmpty(eventModules.Modules);

            // Act/Assert
            Assert.Throws<EventModulesAggregateArgumentException>(() => eventModules.AddModule("Test Module", "This is a second test module"));
        }
    }
}
