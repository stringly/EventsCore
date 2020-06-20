using EventsCore.Domain.Entities;
using EventsCore.Domain.Exceptions.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace EventsCore.Domain.UnitTests.Entities
{
    public class UserTests
    {
        [Fact]
        public void Given_Valid_Values_User_Is_Valid()
        {
            // Arrange
            string ldapName = "User123";
            uint blueDeckId = 1;
            string firstName = "Bob";
            string lastName = "Jones";
            string idNumber = "#1234";
            string email = "bob@test.mail";
            string contactNumber = "1234567890";
            int rankId = 1;

            // Act
            var user = new User(ldapName, blueDeckId, firstName, lastName, idNumber, email, contactNumber, rankId );

            // Assert
            Assert.Equal(ldapName, user.LDAPName);
            Assert.Equal(blueDeckId, user.BlueDeckId);
            Assert.Equal(firstName, user.NameFactory.First);
            Assert.Equal(lastName, user.NameFactory.Last);
            Assert.Equal(idNumber, user.IdNumber);
            Assert.Equal(email, user.Email);
            Assert.Equal(contactNumber, user.ContactNumber);
            Assert.Equal(rankId, user.RankId);
        }
        [Fact]
        public void Should_Throw_UserArgumentException_When_UpdateName_Parameters_Both_Empty()
        {
            // Arrange
            string ldapName = "User123";
            uint blueDeckId = 1;
            string firstName = "Bob";
            string lastName = "Jones";
            string idNumber = "#1234";
            string email = "bob@test.mail";
            string contactNumber = "1234567890";
            int rankId = 1;            
            var user = new User(ldapName, blueDeckId, firstName, lastName, idNumber, email, contactNumber, rankId);            
            Assert.Equal(ldapName, user.LDAPName);
            Assert.Equal(blueDeckId, user.BlueDeckId);
            Assert.Equal(firstName, user.NameFactory.First);
            Assert.Equal(lastName, user.NameFactory.Last);
            Assert.Equal(idNumber, user.IdNumber);
            Assert.Equal(email, user.Email);
            Assert.Equal(contactNumber, user.ContactNumber);
            Assert.Equal(rankId, user.RankId);

            // Act/Assert
            Assert.Throws<UserArgumentException>(() => user.UpdateName("", "      "));
        }
        [Fact]
        public void Should_Throw_UserArgumentException_When_UpdateIdNumber_Parameter_Empty()
        {
            // Arrange
            string ldapName = "User123";
            uint blueDeckId = 1;
            string firstName = "Bob";
            string lastName = "Jones";
            string idNumber = "#1234";
            string email = "bob@test.mail";
            string contactNumber = "1234567890";
            int rankId = 1;
            var user = new User(ldapName, blueDeckId, firstName, lastName, idNumber, email, contactNumber, rankId);
            Assert.Equal(ldapName, user.LDAPName);
            Assert.Equal(blueDeckId, user.BlueDeckId);
            Assert.Equal(firstName, user.NameFactory.First);
            Assert.Equal(lastName, user.NameFactory.Last);
            Assert.Equal(idNumber, user.IdNumber);
            Assert.Equal(email, user.Email);
            Assert.Equal(contactNumber, user.ContactNumber);
            Assert.Equal(rankId, user.RankId);

            // Act/Assert
            Assert.Throws<UserArgumentException>(() => user.UpdateIdNumber(""));
        }
        [Fact]
        public void Should_Throw_UserArgumentException_When_UpdateLDAPName_Parameter_Empty()
        {
            // Arrange
            string ldapName = "User123";
            uint blueDeckId = 1;
            string firstName = "Bob";
            string lastName = "Jones";
            string idNumber = "#1234";
            string email = "bob@test.mail";
            string contactNumber = "1234567890";
            int rankId = 1;
            var user = new User(ldapName, blueDeckId, firstName, lastName, idNumber, email, contactNumber, rankId);
            Assert.Equal(ldapName, user.LDAPName);
            Assert.Equal(blueDeckId, user.BlueDeckId);
            Assert.Equal(firstName, user.NameFactory.First);
            Assert.Equal(lastName, user.NameFactory.Last);
            Assert.Equal(idNumber, user.IdNumber);
            Assert.Equal(email, user.Email);
            Assert.Equal(contactNumber, user.ContactNumber);
            Assert.Equal(rankId, user.RankId);

            // Act/Assert
            Assert.Throws<UserArgumentException>(() => user.UpdateLDAPName(""));
        }
        [Fact]
        public void Should_Throw_UserArgumentException_When_UpdateEmail_Parameter_Empty()
        {
            // Arrange
            string ldapName = "User123";
            uint blueDeckId = 1;
            string firstName = "Bob";
            string lastName = "Jones";
            string idNumber = "#1234";
            string email = "bob@test.mail";
            string contactNumber = "1234567890";
            int rankId = 1;
            var user = new User(ldapName, blueDeckId, firstName, lastName, idNumber, email, contactNumber, rankId);
            Assert.Equal(ldapName, user.LDAPName);
            Assert.Equal(blueDeckId, user.BlueDeckId);
            Assert.Equal(firstName, user.NameFactory.First);
            Assert.Equal(lastName, user.NameFactory.Last);
            Assert.Equal(idNumber, user.IdNumber);
            Assert.Equal(email, user.Email);
            Assert.Equal(contactNumber, user.ContactNumber);
            Assert.Equal(rankId, user.RankId);

            // Act/Assert
            Assert.Throws<UserArgumentException>(() => user.UpdateEmail(""));
        }
        [Fact]
        public void Should_Throw_UserArgumentException_When_UpdateRank_Id_OutOfRange()
        {
            // Arrange
            string ldapName = "User123";
            uint blueDeckId = 1;
            string firstName = "Bob";
            string lastName = "Jones";
            string idNumber = "#1234";
            string email = "bob@test.mail";
            string contactNumber = "1234567890";
            int rankId = 1;
            var user = new User(ldapName, blueDeckId, firstName, lastName, idNumber, email, contactNumber, rankId);
            Assert.Equal(ldapName, user.LDAPName);
            Assert.Equal(blueDeckId, user.BlueDeckId);
            Assert.Equal(firstName, user.NameFactory.First);
            Assert.Equal(lastName, user.NameFactory.Last);
            Assert.Equal(idNumber, user.IdNumber);
            Assert.Equal(email, user.Email);
            Assert.Equal(contactNumber, user.ContactNumber);
            Assert.Equal(rankId, user.RankId);

            // Act/Assert
            Assert.Throws<UserArgumentException>(() => user.UpdateRank(0));
        }
        [Fact]
        public void DisplayName_Returns_Correct_String()
        {
            // Arrange
            string ldapName = "User123";
            uint blueDeckId = 1;
            string firstName = "Bob";
            string lastName = "Jones";
            string idNumber = "1234";
            string email = "bob@test.mail";
            string contactNumber = "1234567890";
            int rankId = 1;
            var user = new User(ldapName, blueDeckId, firstName, lastName, idNumber, email, contactNumber, rankId);
            Assert.Equal(ldapName, user.LDAPName);
            Assert.Equal(blueDeckId, user.BlueDeckId);
            Assert.Equal(firstName, user.NameFactory.First);
            Assert.Equal(lastName, user.NameFactory.Last);
            Assert.Equal(idNumber, user.IdNumber);
            Assert.Equal(email, user.Email);
            Assert.Equal(contactNumber, user.ContactNumber);
            Assert.Equal(rankId, user.RankId);

            // Act
            string result = user.DisplayName;

            // Assert
            Assert.Equal("Bob Jones #1234", result);
        }
        [Fact]
        public void Can_Update_LDAPName()
        {
            // Arrange
            string ldapName = "User123";
            uint blueDeckId = 1;
            string firstName = "Bob";
            string lastName = "Jones";
            string idNumber = "1234";
            string email = "bob@test.mail";
            string contactNumber = "1234567890";
            int rankId = 1;
            var user = new User(ldapName, blueDeckId, firstName, lastName, idNumber, email, contactNumber, rankId);
            Assert.Equal(ldapName, user.LDAPName);
            Assert.Equal(blueDeckId, user.BlueDeckId);
            Assert.Equal(firstName, user.NameFactory.First);
            Assert.Equal(lastName, user.NameFactory.Last);
            Assert.Equal(idNumber, user.IdNumber);
            Assert.Equal(email, user.Email);
            Assert.Equal(contactNumber, user.ContactNumber);
            Assert.Equal(rankId, user.RankId);

            // Act
            string newLDAP = "User234";
            user.UpdateLDAPName(newLDAP);

            // Assert
            Assert.Equal(newLDAP, user.LDAPName);
        }
        [Fact]
        public void Can_Update_BlueDeckId()
        {
            // Arrange
            string ldapName = "User123";
            uint blueDeckId = 1;
            string firstName = "Bob";
            string lastName = "Jones";
            string idNumber = "1234";
            string email = "bob@test.mail";
            string contactNumber = "1234567890";
            int rankId = 1;
            var user = new User(ldapName, blueDeckId, firstName, lastName, idNumber, email, contactNumber, rankId);
            Assert.Equal(ldapName, user.LDAPName);
            Assert.Equal(blueDeckId, user.BlueDeckId);
            Assert.Equal(firstName, user.NameFactory.First);
            Assert.Equal(lastName, user.NameFactory.Last);
            Assert.Equal(idNumber, user.IdNumber);
            Assert.Equal(email, user.Email);
            Assert.Equal(contactNumber, user.ContactNumber);
            Assert.Equal(rankId, user.RankId);

            // Act
            uint newBlueDeckId = 2;
            user.UpdateBlueDeckId(newBlueDeckId);

            // Assert
            Assert.Equal(newBlueDeckId, user.BlueDeckId);
        }
        [Fact]
        public void Can_Update_FirstName()
        {
            // Arrange
            string ldapName = "User123";
            uint blueDeckId = 1;
            string firstName = "Bob";
            string lastName = "Jones";
            string idNumber = "1234";
            string email = "bob@test.mail";
            string contactNumber = "1234567890";
            int rankId = 1;
            var user = new User(ldapName, blueDeckId, firstName, lastName, idNumber, email, contactNumber, rankId);
            Assert.Equal(ldapName, user.LDAPName);
            Assert.Equal(blueDeckId, user.BlueDeckId);
            Assert.Equal(firstName, user.NameFactory.First);
            Assert.Equal(lastName, user.NameFactory.Last);
            Assert.Equal(idNumber, user.IdNumber);
            Assert.Equal(email, user.Email);
            Assert.Equal(contactNumber, user.ContactNumber);
            Assert.Equal(rankId, user.RankId);

            // Act
            string newFirstName = "Steve";
            user.UpdateName(newFirstName);

            // Assert
            Assert.Equal(newFirstName, user.NameFactory.First);
            Assert.Equal(lastName, user.NameFactory.Last);
        }
        [Fact]
        public void Can_Update_LastName()
        {
            // Arrange
            string ldapName = "User123";
            uint blueDeckId = 1;
            string firstName = "Bob";
            string lastName = "Jones";
            string idNumber = "1234";
            string email = "bob@test.mail";
            string contactNumber = "1234567890";
            int rankId = 1;
            var user = new User(ldapName, blueDeckId, firstName, lastName, idNumber, email, contactNumber, rankId);
            Assert.Equal(ldapName, user.LDAPName);
            Assert.Equal(blueDeckId, user.BlueDeckId);
            Assert.Equal(firstName, user.NameFactory.First);
            Assert.Equal(lastName, user.NameFactory.Last);
            Assert.Equal(idNumber, user.IdNumber);
            Assert.Equal(email, user.Email);
            Assert.Equal(contactNumber, user.ContactNumber);
            Assert.Equal(rankId, user.RankId);

            // Act
            string newLastName = "Smith";
            user.UpdateName("", newLastName);

            // Assert
            Assert.Equal(newLastName, user.NameFactory.Last);
            Assert.Equal(firstName, user.NameFactory.First);
        }
        [Fact]
        public void Can_Update_First_And_Last_Names()
        {
            // Arrange
            string ldapName = "User123";
            uint blueDeckId = 1;
            string firstName = "Bob";
            string lastName = "Jones";
            string idNumber = "1234";
            string email = "bob@test.mail";
            string contactNumber = "1234567890";
            int rankId = 1;
            var user = new User(ldapName, blueDeckId, firstName, lastName, idNumber, email, contactNumber, rankId);
            Assert.Equal(ldapName, user.LDAPName);
            Assert.Equal(blueDeckId, user.BlueDeckId);
            Assert.Equal(firstName, user.NameFactory.First);
            Assert.Equal(lastName, user.NameFactory.Last);
            Assert.Equal(idNumber, user.IdNumber);
            Assert.Equal(email, user.Email);
            Assert.Equal(contactNumber, user.ContactNumber);
            Assert.Equal(rankId, user.RankId);

            // Act
            string newFirstName = "Steve";
            string newLastName = "Smith";
            user.UpdateName(newFirstName, newLastName);

            // Assert
            Assert.Equal(newFirstName, user.NameFactory.First);
            Assert.Equal(newLastName, user.NameFactory.Last);
        }
        [Fact]
        public void Can_Update_IdNumber()
        {
            // Arrange
            string ldapName = "User123";
            uint blueDeckId = 1;
            string firstName = "Bob";
            string lastName = "Jones";
            string idNumber = "1234";
            string email = "bob@test.mail";
            string contactNumber = "1234567890";
            int rankId = 1;
            var user = new User(ldapName, blueDeckId, firstName, lastName, idNumber, email, contactNumber, rankId);
            Assert.Equal(ldapName, user.LDAPName);
            Assert.Equal(blueDeckId, user.BlueDeckId);
            Assert.Equal(firstName, user.NameFactory.First);
            Assert.Equal(lastName, user.NameFactory.Last);
            Assert.Equal(idNumber, user.IdNumber);
            Assert.Equal(email, user.Email);
            Assert.Equal(contactNumber, user.ContactNumber);
            Assert.Equal(rankId, user.RankId);

            // Act
            string newIdNumber = "2345";
            user.UpdateIdNumber(newIdNumber);

            // Assert
            Assert.Equal(newIdNumber, user.IdNumber);
        }
        [Fact]
        public void Can_Update_Email()
        {
            // Arrange
            string ldapName = "User123";
            uint blueDeckId = 1;
            string firstName = "Bob";
            string lastName = "Jones";
            string idNumber = "1234";
            string email = "bob@test.mail";
            string contactNumber = "1234567890";
            int rankId = 1;
            var user = new User(ldapName, blueDeckId, firstName, lastName, idNumber, email, contactNumber, rankId);
            Assert.Equal(ldapName, user.LDAPName);
            Assert.Equal(blueDeckId, user.BlueDeckId);
            Assert.Equal(firstName, user.NameFactory.First);
            Assert.Equal(lastName, user.NameFactory.Last);
            Assert.Equal(idNumber, user.IdNumber);
            Assert.Equal(email, user.Email);
            Assert.Equal(contactNumber, user.ContactNumber);
            Assert.Equal(rankId, user.RankId);

            // Act
            string newEmail = "newEmail@mail.com";
            user.UpdateEmail(newEmail);

            // Assert
            Assert.Equal(newEmail, user.Email);
        }
        [Fact]
        public void Can_Update_ContactNumber()
        {
            // Arrange
            string ldapName = "User123";
            uint blueDeckId = 1;
            string firstName = "Bob";
            string lastName = "Jones";
            string idNumber = "1234";
            string email = "bob@test.mail";
            string contactNumber = "1234567890";
            int rankId = 1;
            var user = new User(ldapName, blueDeckId, firstName, lastName, idNumber, email, contactNumber, rankId);
            Assert.Equal(ldapName, user.LDAPName);
            Assert.Equal(blueDeckId, user.BlueDeckId);
            Assert.Equal(firstName, user.NameFactory.First);
            Assert.Equal(lastName, user.NameFactory.Last);
            Assert.Equal(idNumber, user.IdNumber);
            Assert.Equal(email, user.Email);
            Assert.Equal(contactNumber, user.ContactNumber);
            Assert.Equal(rankId, user.RankId);

            // Act
            string newNumber = "1112223333";
            user.UpdateContactNumber(newNumber);

            // Assert
            Assert.Equal(newNumber, user.ContactNumber);
        }
        [Fact]
        public void Can_Update_Rank_By_Id()
        {
            // Arrange
            string ldapName = "User123";
            uint blueDeckId = 1;
            string firstName = "Bob";
            string lastName = "Jones";
            string idNumber = "1234";
            string email = "bob@test.mail";
            string contactNumber = "1234567890";
            int rankId = 1;
            var user = new User(ldapName, blueDeckId, firstName, lastName, idNumber, email, contactNumber, rankId);
            Assert.Equal(ldapName, user.LDAPName);
            Assert.Equal(blueDeckId, user.BlueDeckId);
            Assert.Equal(firstName, user.NameFactory.First);
            Assert.Equal(lastName, user.NameFactory.Last);
            Assert.Equal(idNumber, user.IdNumber);
            Assert.Equal(email, user.Email);
            Assert.Equal(contactNumber, user.ContactNumber);
            Assert.Equal(rankId, user.RankId);

            // Act
            int newRankId = 2;
            user.UpdateRank(newRankId);

            // Assert
            Assert.Equal(newRankId, user.RankId);
        }
        [Fact]
        public void Can_Add_User_To_Role()
        {
            // Arrange
            string ldapName = "User123";
            uint blueDeckId = 1;
            string firstName = "Bob";
            string lastName = "Jones";
            string idNumber = "1234";
            string email = "bob@test.mail";
            string contactNumber = "1234567890";
            int rankId = 1;
            var user = new User(ldapName, blueDeckId, firstName, lastName, idNumber, email, contactNumber, rankId);
            var role = new UserRoleType { Name = "Administrator" };

            // Act
            user.AddToRole(role);

            // Assert
            Assert.Single(user.Roles);
        }
        [Fact]
        public void Can_Remove_User_From_Role()
        {
            // Arrange
            string ldapName = "User123";
            uint blueDeckId = 1;
            string firstName = "Bob";
            string lastName = "Jones";
            string idNumber = "1234";
            string email = "bob@test.mail";
            string contactNumber = "1234567890";
            int rankId = 1;
            var user = new User(ldapName, blueDeckId, firstName, lastName, idNumber, email, contactNumber, rankId);
            var role = new UserRoleType { Name = "Administrator" };
            user.AddToRole(role);
            Assert.Single(user.Roles);
            // Act
            user.RemoveFromRole(role);

            // Assert
            Assert.Empty(user.Roles);
        }
        [Fact]
        public void Should_Throw_UserArgumentException_When_Removing_User_From_NonExisting_Role()
        {
            // Arrange
            string ldapName = "User123";
            uint blueDeckId = 1;
            string firstName = "Bob";
            string lastName = "Jones";
            string idNumber = "1234";
            string email = "bob@test.mail";
            string contactNumber = "1234567890";
            int rankId = 1;
            var user = new User(ldapName, blueDeckId, firstName, lastName, idNumber, email, contactNumber, rankId);
            var role = new UserRoleType { Name = "Administrator" };

            // Act/Assert
            Assert.Throws<UserArgumentException>(() => user.RemoveFromRole(role));
        }
    }
}
