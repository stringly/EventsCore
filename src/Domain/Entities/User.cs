using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.User;
using EventsCore.Domain.ValueObjects;
using System;

namespace EventsCore.Domain.Entities
{
    /// <summary>
    /// An Entity class representing a User
    /// </summary>
    public class User : IEntity, IAggregateRoot
    {
        private User() { }
        /// <summary>
        /// Creates a new instance of the User object
        /// </summary>
        /// <param name="LDAPName">A string containing the User's LDAP name.</param>
        /// <param name="blueDeckId">A string containing the User's BlueDeck Id.</param>
        /// <param name="firstName">A string containing the User's first name.</param>
        /// <param name="lastName">A string containing the User's last name.</param>
        /// <param name="idNumber">A string containing the User's Id Number.</param>
        /// <param name="email">A string containing the User's email address.</param>
        /// <param name="contactNumber">A string containing the User's contact number.</param>
        /// <param name="rankId">An integer containing the Id of the User's <see cref="Rank"></see> Rank.</param>
        public User(string LDAPName, uint blueDeckId, string firstName, string lastName, string idNumber, string email, string contactNumber, int rankId)
        {
            UpdateLDAPName(LDAPName);
            UpdateBlueDeckId(blueDeckId);
            UpdateName(firstName, lastName);
            UpdateIdNumber(idNumber);
            UpdateEmail(email);
            UpdateContactNumber(contactNumber);
            UpdateRank(rankId);
        }
        /// <summary>
        /// The Id of this User's instance.
        /// </summary>
        public int Id { get; private set; }
        private string _LDAPName;
        /// <summary>
        /// Returns a string containing the User's LDAP Name.
        /// </summary>
        public string LDAPName => _LDAPName;
        private uint _blueDeckId;
        /// <summary>
        /// Returns the User's BlueDeck Id.
        /// </summary>
        public uint BlueDeckId => _blueDeckId;
        /// <summary>
        /// Returns the Person's <see cref="PersonFullName"></see>
        /// </summary>
        public PersonFullName NameFactory { get; private set; }
        /// <summary>
        /// Returns the User's full name.
        /// </summary>
        public string Name => NameFactory.FullName;
        private string _idNumber;
        /// <summary>
        /// Returns the User's Id Number.
        /// </summary>
        public string IdNumber => _idNumber;
        private string _email;
        /// <summary>
        /// Returns the User's email address.
        /// </summary>
        public string Email => _email;
        private string _contactNumber;
        /// <summary>
        /// Returns the User's contact number.
        /// </summary>
        public string ContactNumber => _contactNumber;
        /// <summary>
        /// Returns the Id of the User's <see cref="Rank"></see>
        /// </summary>
        public int? RankId { get; private set; }
        /// <summary>
        /// Returns the User's <see cref="Rank"></see>
        /// </summary>
        public Rank Rank { get; private set; }
        /// <summary>
        /// Returns a string containing the User's Display Name
        /// </summary>
        public string DisplayName => $"{Rank?.Abbreviation ?? ""} {Name} {(String.IsNullOrEmpty(IdNumber) ? "" : $"#{IdNumber}")}";
        /// <summary>
        /// Updates the User's LDAP Name
        /// </summary>
        /// <param name="newName"></param>
        /// <exception cref="UserArgumentException">Thrown when the newName parameter is empty/whitespace.</exception>
        public void UpdateLDAPName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new UserArgumentException("Cannot set field to empty string", nameof(newName));
            }
            else
            {
                _LDAPName = newName;
            }
        }
        /// <summary>
        /// Updates the User's BlueDeckId
        /// </summary>
        /// <param name="newId"></param>
        public void UpdateBlueDeckId(uint newId)
        {
            _blueDeckId = newId;
        }
        /// <summary>
        /// Updates the User's First/Last Name
        /// </summary>
        /// <param name="firstName">A string containing the User's new first name.</param>
        /// <param name="lastName">A string containing the User's new last name.</param>
        /// <exception cref="UserArgumentException">Thrown when both the firstName and lastName are empty/whitespace.</exception>
        public void UpdateName(string firstName, string lastName)
        {
            if(string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
            {
                throw new UserArgumentException("Cannot update User name: at least one parameter must not be null/empty string.", nameof(firstName));
            }
            else if(!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                NameFactory = new PersonFullName(firstName, lastName);
            }
            else if(!string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
            {
                NameFactory = new PersonFullName(firstName, NameFactory.Last);
            }
            else if(string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                NameFactory = new PersonFullName(NameFactory.First, lastName);
            }
        }
        /// <summary>
        /// Updates the User's Id Number
        /// </summary>
        /// <param name="newId">A string containing the new Id Number</param>
        /// <exception cref="UserArgumentException">Thrown when the newId is empty/whitespace.</exception>
        public void UpdateIdNumber(string newId)
        {            
            _idNumber = !string.IsNullOrWhiteSpace(newId) ? newId : throw new UserArgumentException("Cannot update User IdNumber: parameter must not be null/empty string.", nameof(newId));
        }
        /// <summary>
        /// Updates the User's email
        /// </summary>
        /// <param name="newEmail">A string containing the new email.</param>
        /// <exception cref="UserArgumentException">Thrown when the newEmail parameter is empty/whitespace.</exception>
        public void UpdateEmail(string newEmail)
        {
            _email = !string.IsNullOrWhiteSpace(newEmail) ? newEmail : throw new UserArgumentException("Cannot set email to null or empty string", nameof(newEmail));            
        }
        /// <summary>
        /// Updates the User's Contact number
        /// </summary>
        /// <param name="newNumber">A string containing the new contact number.</param>        
        public void UpdateContactNumber(string newNumber)
        {
            _contactNumber = newNumber;
        }
        /// <summary>
        /// Updates the User's Rank.
        /// </summary>
        /// <param name="rankId">An integer representing the Id of a <see cref="Rank"></see></param>
        /// <exception cref="UserArgumentException">Thrown when the rankId parameter is 0 or out of range.</exception>
        public void UpdateRank(int rankId)
        {
            RankId = rankId != 0 ? rankId : throw new UserArgumentException("Cannot update User Rank: parameter must not be 0.", nameof(rankId));
        }
    }
}
