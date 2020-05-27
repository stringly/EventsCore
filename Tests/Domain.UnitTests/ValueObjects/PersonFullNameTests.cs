using EventsCore.Domain.Exceptions.ValueObjects;
using Xunit;

namespace EventsCore.Domain.ValueObjects
{
    public class PersonFullNameTests
    {
        [Fact]
        public void PersonFullName_Given_Valid_Names_Is_Valid()
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";

            // Act
            var personFullName = new PersonFullName(firstName, lastName);

            // Assert
            Assert.Equal(personFullName.First, firstName);
            Assert.Equal(personFullName.Last, lastName);
        }

        [Fact]
        public void Should_Throw_PersonFullNameInvalidException_For_Empty_FirstName()
        {
            // Arrange
            string firstName = "";
            string lastName = "Doe";
            
            // Act/Assert
            Assert.Throws<PersonFullNameException>(() => new PersonFullName(firstName, lastName));
        }

        [Fact]
        public void Should_Throw_PersonFullNameInvalidException_For_Empty_LastName()
        {
            // Arrange
            string firstName = "John";
            string lastName = "";
            
            // Act/Assert
            Assert.Throws<PersonFullNameException>(() => new PersonFullName(firstName, lastName));
        }

        [Fact]
        public void PersonFullName_FullName_Returns_Correct_String()
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";

            // Act
            var personFullName = new PersonFullName(firstName, lastName);

            // Assert
            Assert.Equal("John Doe", personFullName.FullName);
        }

        [Fact]
        public void PersonFullName_FullNameReverse_Returns_Correct_String()
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";

            // Act
            var personFullName = new PersonFullName(firstName, lastName);

            // Assert
            Assert.Equal("Doe, John", personFullName.FullNameReverse);
        }
    }
}
