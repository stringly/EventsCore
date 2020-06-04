using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.Rank;

namespace EventsCore.Domain.Entities
{
    /// <summary>
    /// Entity class representing a User's rank
    /// </summary>
    public class Rank : BaseEntity, IAggregateRoot
    {
        private Rank() { }
        /// <summary>
        /// Creates a new Rank instance
        /// </summary>
        /// <param name="abbreviation">A string containing the abbreviation for the Rank.</param>
        /// <param name="fullName">A string containing the full name of the Rank.</param>
        public Rank(string fullName, string abbreviation)
        {
            UpdateAbbreviation(abbreviation);
            UpdateFullName(fullName);
        }
        private string _abbreviation;
        /// <summary>
        /// Returns a string containing the Rank's abbreviation.
        /// </summary>
        public string Abbreviation => _abbreviation;
        private string _fullName;
        /// <summary>
        /// Returns a string containing the Rank's Full Name.
        /// </summary>
        public string FullName => _fullName;
        /// <summary>
        /// Updates the abbreviation of the Rank.
        /// </summary>
        /// <param name="newAbbrev">A string containing the new abbreviation.</param>
        /// <exception cref="RankArgumentException">Thrown when the newAbbrev parameter is empty/whitespace.</exception>
        public void UpdateAbbreviation(string newAbbrev)
        {
            _abbreviation = !string.IsNullOrWhiteSpace(newAbbrev) ? newAbbrev : throw new RankArgumentException("Cannot update Rank: parameter cannot be null/empty string.", nameof(newAbbrev));
        }
        /// <summary>
        /// Updates the name of the Rank.
        /// </summary>
        /// <param name="newName">A string containing the new Rank name.</param>
        /// <exception cref="RankArgumentException">Thrown when the newName parameter is empty/whitespace.</exception>
        public void UpdateFullName(string newName)
        {
            _fullName = !string.IsNullOrWhiteSpace(newName) ? newName : throw new RankArgumentException("Cannot update Rank: parameter cannot be null/empty string.", nameof(newName));
        }
    }
}
