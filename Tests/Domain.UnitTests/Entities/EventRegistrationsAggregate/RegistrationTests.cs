using EventsCore.Domain.Common;
using EventsCore.Domain.Entities.EventRegistrationsAggregate;
using EventsCore.Domain.Exceptions.EventRegistrationsAggregate;
using EventsCore.Domain.UnitTests.Common;
using Xunit;

namespace EventsCore.Domain.UnitTests.Entities.EventRegistrationsAggregate
{
    public class RegistrationTests
    {
        private readonly IDateTime _dateTime;
        public RegistrationTests()
        {
            _dateTime = new DateTimeTestProvider();
        }
        [Fact]
        public void Given_Valid_Values_Registration_Is_Valid()
        {
            // Arrange
            int userId = 1;
            string userName = "Test User";
            string email = "testuser@mail.com";
            string contact = "1234567890";
            
            // Act
            var reg = new Registration(userId, userName, email, contact, _dateTime);

            // Assert
            Assert.Equal(1, reg.UserId);
            Assert.Equal("Test User", reg.UserName);
            Assert.Equal("testuser@mail.com", reg.Email);
            Assert.Equal("1234567890", reg.Contact);
            Assert.Equal(_dateTime.Now, reg.Registered);
            Assert.Equal(_dateTime.Now, reg.StatusChanged);
            Assert.Equal(RegistrationStatus.Pending, reg.Status);
        }
        [Fact]
        public void Should_Throw_EventRegistrationAggregateArgumentException_For_UserId_OutOfRange()
        {
            // Arrange
            int userId = 0;
            string userName = "Test User";
            string email = "testuser@mail.com";
            string contact = "1234567890";

            // Act/Assert
            Assert.Throws<EventRegistrationAggregateArgumentException>(() => new Registration(userId, userName, email, contact, _dateTime));
        }
        [Fact]
        public void Should_Throw_EventRegistrationAggregateArgumentException_For_Empty_UserName()
        {
            // Arrange
            int userId = 1;
            string userName = "";
            string email = "testuser@mail.com";
            string contact = "1234567890";

            // Act/Assert
            Assert.Throws<EventRegistrationAggregateArgumentException>(() => new Registration(userId, userName, email, contact, _dateTime));
        }
        [Fact]
        public void Should_Throw_EventRegistrationAggregateArgumentException_For_Empty_Email()
        {
            // Arrange
            int userId = 1;
            string userName = "Test User";
            string email = "";
            string contact = "1234567890";

            // Act/Assert
            Assert.Throws<EventRegistrationAggregateArgumentException>(() => new Registration(userId, userName, email, contact, _dateTime));
        }
        [Fact]
        public void Can_Update_RegistrationStatus_Accepted()
        {
            // Arrange
            int userId = 1;
            string userName = "Test User";
            string email = "testuser@mail.com";
            string contact = "1234567890";
            var reg = new Registration(userId, userName, email, contact, _dateTime);
            Assert.Equal(RegistrationStatus.Pending, reg.Status);

            // Act
            reg.UpdateStatusAccepted();

            // Assert 
            Assert.Equal(RegistrationStatus.Accepted, reg.Status);

        }
        [Fact]
        public void Can_Update_RegistrationStatus_Pending()
        {
            // Arrange
            int userId = 1;
            string userName = "Test User";
            string email = "testuser@mail.com";
            string contact = "1234567890";
            var reg = new Registration(userId, userName, email, contact, _dateTime);
            Assert.Equal(RegistrationStatus.Pending, reg.Status);
            reg.UpdateStatusAccepted(); // regs are defaulted to Pending Status, so we need to change the status to change it back
            Assert.Equal(RegistrationStatus.Accepted, reg.Status);

            // Act
            reg.UpdateStatusPending();

            // Assert 
            Assert.Equal(RegistrationStatus.Pending, reg.Status);
        }
        [Fact]
        public void Can_Update_RegistrationStatus_Standby()
        {
            // Arrange
            int userId = 1;
            string userName = "Test User";
            string email = "testuser@mail.com";
            string contact = "1234567890";
            var reg = new Registration(userId, userName, email, contact, _dateTime);
            Assert.Equal(RegistrationStatus.Pending, reg.Status);            

            // Act
            reg.UpdateStatusStandby();

            // Assert 
            Assert.Equal(RegistrationStatus.Standby, reg.Status);
        }
        [Fact]
        public void Can_Update_RegistrationStatus_Rejected()
        {
            // Arrange
            int userId = 1;
            string userName = "Test User";
            string email = "testuser@mail.com";
            string contact = "1234567890";
            var reg = new Registration(userId, userName, email, contact, _dateTime);
            Assert.Equal(RegistrationStatus.Pending, reg.Status);            

            // Act
            reg.UpdateStatusRejected();

            // Assert 
            Assert.Equal(RegistrationStatus.Rejected, reg.Status);
        }
    }
}
