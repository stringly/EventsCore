using EventsCore.Domain.Entities;
using EventsCore.Domain.Exceptions.Registration;
using EventsCore.Domain.UnitTests.Common;
using Xunit;

namespace EventsCore.Domain.UnitTests.Entities
{
    public class RegistrationTests
    {
        [Fact]
        public void Given_Valid_Values_Registration_Is_Valid()
        {
            // Arrange
            var validUserId = 1;
            var validUserName = "Bob Smith";
            var validUserEmail = "Bob@mail.com";
            var validUserContact = "123456789";
            var dateTime = new DateTimeTestProvider();

            // Act
            var entity = new Registration(validUserId, validUserName, validUserEmail, validUserContact, dateTime);

            // Assert
            Assert.Equal(validUserId, entity.UserId);
            Assert.Equal(validUserName, entity.UserName);
            Assert.Equal(validUserEmail, entity.Email);
            Assert.Equal(validUserContact, entity.Contact);
            Assert.Equal(dateTime.Now, entity.Registered);
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Given_Invalid_UserId_Throws_RegistrationArgumentException(int value)
        {
            // Arrange
            var inValidUserId = value;
            var validUserName = "Bob Smith";
            var validUserEmail = "Bob@mail.com";
            var validUserContact = "123456789";
            var dateTime = new DateTimeTestProvider();

            // Act/Assert
            Assert.Throws<RegistrationArgumentException>(() => new Registration(inValidUserId, validUserName, validUserEmail, validUserContact, dateTime));
        }
        [Theory]
        [InlineData("")]
        [InlineData("                      ")]
        public void Given_Invalid_UserName_Throws_RegistrationArgumentException(string value)
        {
            // Arrange
            var validUserId = 1;
            var inValidUserName = value;
            var validUserEmail = "Bob@mail.com";
            var validUserContact = "123456789";
            var dateTime = new DateTimeTestProvider();

            // Act/Assert
            Assert.Throws<RegistrationArgumentException>(() => new Registration(validUserId, inValidUserName, validUserEmail, validUserContact, dateTime));
        }
        [Theory]
        [InlineData("")]
        [InlineData("                      ")]
        public void Given_Invalid_UserEmail_Throws_RegistrationArgumentException(string value)
        {
            // Arrange
            var validUserId = 1;
            var validUserName = "Bob@mail.com";
            var inValidUserEmail = value;
            var validUserContact = "123456789";
            var dateTime = new DateTimeTestProvider();

            // Act/Assert
            Assert.Throws<RegistrationArgumentException>(() => new Registration(validUserId, validUserName, inValidUserEmail, validUserContact, dateTime));
        }

    }

}
