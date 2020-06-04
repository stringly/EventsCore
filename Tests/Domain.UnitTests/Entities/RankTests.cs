using EventsCore.Domain.Entities;
using EventsCore.Domain.Exceptions.Rank;
using Xunit;

namespace EventsCore.Domain.UnitTests.Entities
{
    public class RankTests
    {
        [Fact]
        public void Given_Valid_Values_Rank_Is_Valid()
        {
            // Arrange
            string name = "New Rank";
            string abbrev = "N.R.";

            // Act
            var rank = new Rank(name, abbrev);

            // Assert
            Assert.Equal("New Rank", rank.FullName);
            Assert.Equal("N.R.", rank.Abbreviation);
        }
        [Fact]
        public void Should_Throw_RankArgumentException_For_Empty_Name()
        {
            // Arrange
            string name = "";
            string abbrev = "N.R.";

            // Act/Assert
            Assert.Throws<RankArgumentException>(() => new Rank(name, abbrev));
        }
        [Fact]
        public void Should_Throw_RankArgumentException_For_Empty_Abbreviation()
        {
            // Arrange
            string name = "New Rank";
            string abbrev = "      ";

            // Act/Assert
            Assert.Throws<RankArgumentException>(() => new Rank(name, abbrev));
        }
        [Fact]
        public void Can_Update_Rank_Name()
        {
            // Arrange
            string name = "New Rank";
            string abbrev = "N.R.";            
            var rank = new Rank(name, abbrev);
            Assert.Equal("New Rank", rank.FullName);
            Assert.Equal("N.R.", rank.Abbreviation);
            string newName = "Updated Rank";
            // Act
            rank.UpdateFullName(newName);

            // Assert
            Assert.Equal(newName, rank.FullName);
        }
        [Fact]
        public void Can_Update_Rank_Abbreviation()
        {
            // Arrange
            string name = "New Rank";
            string abbrev = "N.R.";
            var rank = new Rank(name, abbrev);
            Assert.Equal("New Rank", rank.FullName);
            Assert.Equal("N.R.", rank.Abbreviation);
            string newAbbrev = "U.R.";
            // Act
            rank.UpdateAbbreviation(newAbbrev);

            // Assert
            Assert.Equal(newAbbrev, rank.Abbreviation);
        }
    }
}
