using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.Rank;

namespace EventsCore.Domain.Entities
{
    public class Rank : IEntity, IAggregateRoot
    {
        private Rank() { }
        public Rank(string abbreviation, string fullName)
        {
            UpdateAbbreviation(abbreviation);
            UpdateFullName(fullName);
        }
        public int Id { get; private set; }
        private string _abbreviation;
        public string Abbreviation => _abbreviation;
        private string _fullName;
        public string FullName => _fullName;
        public void UpdateAbbreviation(string newAbbrev)
        {
            _abbreviation = !string.IsNullOrWhiteSpace(newAbbrev) ? newAbbrev : throw new RankArgumentException("Cannot update Rank: parameter cannot be null/empty string.", nameof(newAbbrev));
        }
        public void UpdateFullName(string newName)
        {
            _fullName = !string.IsNullOrWhiteSpace(newName) ? newName : throw new RankArgumentException("Cannot update Rank: parameter cannot be null/empty string.", nameof(newName));
        }
    }
}
