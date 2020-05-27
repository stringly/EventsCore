using EventsCore.Domain.Exceptions.ValueObjects;
using EventsCore.Domain.ValueObjects;
using Xunit;

namespace EventsCore.Domain.UnitTests.ValueObjects
{
    public class AddressTests
    {
        [Fact]
        public void Address_Given_Valid_Arguments_Returns_Valid()
        {
            // Arrange
            string Street = "123 Anywhere St.";
            string Suite = "";
            string City = "Yourtown";
            string State = "MD";
            string Zip = "12345";

            // Act
            var Address = new Address(Street, Suite, City, State, Zip);

            // Assert
            Assert.Equal("123 Anywhere St.", Address.Street);
            Assert.Equal("", Address.Suite);
            Assert.Equal("Yourtown", Address.City);
            Assert.Equal("MD", Address.State);
            Assert.Equal("12345", Address.ZipCode);
        }

        [Fact]
        public void Should_Throw_AddressInvalidException_For_Empty_Street()
        {
            // Arrange
            string Street = "";
            string Suite = "#3";
            string City = "Yourtown";
            string State = "MD";
            string Zip = "12345";            

            // Act/Assert
            Assert.Throws<AddressInvalidException>(() => new Address(Street, Suite, City, State, Zip));
        }

        [Fact]
        public void Should_Throw_AddressInvalidException_For_Empty_City()
        {
            // Arrange
            string Street = "123 Anywhere St.";
            string Suite = "#3";
            string City = "";
            string State = "MD";
            string Zip = "12345";

            // Act/Assert
            Assert.Throws<AddressInvalidException>(() => new Address(Street, Suite, City, State, Zip));
        }

        [Fact]
        public void Should_Throw_AddressInvalidException_For_Empty_State()
        {
            // Arrange
            string Street = "123 Anywhere St.";
            string Suite = "#3";
            string City = "Yourtown";
            string State = "";
            string Zip = "12345";

            // Act/Assert
            Assert.Throws<AddressInvalidException>(() => new Address(Street, Suite, City, State, Zip));
        }

        [Fact]
        public void Should_Throw_AddressInvalidException_For_Empty_ZipCode()
        {
            // Arrange
            string Street = "123 Anywhere St.";
            string Suite = "#3";
            string City = "Yourtown";
            string State = "MD";
            string Zip = "";

            // Act/Assert
            Assert.Throws<AddressInvalidException>(() => new Address(Street, Suite, City, State, Zip));
        }
    }
}
