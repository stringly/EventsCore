using EventsCore.Domain.Common;
using System.Collections.Generic;
using Xunit;

namespace EventsCore.Domain.UnitTests.Common
{
    public class ValueObjectTests
    {            
        [Fact]
        public void Equals_GivenDifferentValues_ShouldReturnFalse()
        {
            // Arrange/Act
            var point1 = new Point(1, 2);
            var point2 = new Point(2, 1);

            // Assert
            Assert.False(point1.Equals(point2));
        }

        [Fact]
        public void Equals_GivenMatchingValues_ShouldReturnTrue()
        {
            // Arrange/Act
            var point1 = new Point(1, 2);
            var point2 = new Point(1, 2);

            // Assert
            Assert.True(point1.Equals(point2));
        }

        /// <summary>
        /// Test implementation of ValueObject as a Point
        /// </summary>
        private class Point : ValueObject
        {            
            public int X { get; set; }
            public int Y { get; set; }

            
            private Point() { }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            protected override IEnumerable<object> GetAtomicValues()
            {
                yield return X;
                yield return Y;
            }
        }
    }
}
