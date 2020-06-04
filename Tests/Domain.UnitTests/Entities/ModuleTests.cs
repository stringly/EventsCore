using EventsCore.Domain.Entities;
using EventsCore.Domain.Exceptions.Module;
using Xunit;

namespace EventsCore.Domain.UnitTests.Entities
{
    public class ModuleTests
    {
        [Fact]
        public void Given_Valid_Values_Module_Is_Valid()
        {
            // Arrange
            string name = "Test Module";
            string description = "This is a test module.";

            // Act
            var module = new Module(name, description);

            // Assert
            Assert.Equal("Test Module", module.ModuleName);
            Assert.Equal("This is a test module.", module.Description);
        }

        [Fact]
        public void Should_Throw_ModuleArgumentException_For_Empty_ModuleName()
        {
            // Arrange
            string name = "";
            string description = "This is a test module.";

            // Act/Assert
            Assert.Throws<ModuleArgumentException>(() => new Module(name, description));
        }

        [Fact]
        public void Should_Throw_ModuleArgumentException_For_Empty_Description()
        {
            // Arrange
            string name = "Test Module";
            string description = "";

            // Act/Assert
            Assert.Throws<ModuleArgumentException>(() => new Module(name, description));
        }
    }
}

