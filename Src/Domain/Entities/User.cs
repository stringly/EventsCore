using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.User;
using EventsCore.Domain.ValueObjects;
using System;

namespace EventsCore.Domain.Entities
{
    public class User : IEntity, IAggregateRoot
    {
        private User() { }

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

        public int Id { get; private set; }
        private string _LDAPName;
        public string LDAPName => _LDAPName;
        private uint _blueDeckId;
        public uint BlueDeckId => _blueDeckId;
        public PersonFullName NameFactory { get; private set; }
        public string Name => NameFactory.FullName;
        private string _idNumber;
        public string IdNumber => _idNumber;
        private string _email;
        public string Email => _email;
        private string _contactNumber;
        public string ContactNumber => _contactNumber;
        public int? RankId { get; private set; }
        public Rank Rank { get; private set; }
        public string DisplayName => $"{Rank?.Abbreviation ?? ""} {Name} {(String.IsNullOrEmpty(IdNumber) ? "" : $"#{IdNumber}")}";

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
        public void UpdateBlueDeckId(uint newId)
        {
            _blueDeckId = newId;
        }
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
        public void UpdateIdNumber(string newId)
        {            
            _idNumber = !string.IsNullOrWhiteSpace(newId) ? newId : throw new UserArgumentException("Cannot update User IdNumber: parameter must not be null/empty string.", nameof(newId));
        }
        public void UpdateEmail(string newEmail)
        {
            _email = !string.IsNullOrWhiteSpace(newEmail) ? newEmail : throw new UserArgumentException("Cannot set email to null or empty string", nameof(newEmail));            
        }
        public void UpdateContactNumber(string newNumber)
        {
            _contactNumber = newNumber;
        }
        public void UpdateRank(int rankId)
        {
            RankId = rankId != 0 ? rankId : throw new UserArgumentException("Cannot update User Rank: parameter must not be 0.", nameof(rankId));
        }
    }
}
