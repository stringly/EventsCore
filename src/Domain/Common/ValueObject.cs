using System.Collections.Generic;
using System.Linq;

namespace EventsCore.Domain.Common
{
    /// <summary>
    /// Abstract base class for a ValueObject object
    /// </summary>
    public abstract class ValueObject
    {
        /// <summary>
        /// Determines if two ValueObjects are equal
        /// </summary>
        /// <param name="left">The left ValueObject in the comparison</param>
        /// <param name="right">The right ValueObject in the comparison</param>
        /// <returns></returns>
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }

            return left?.Equals(right) != false;
        }
        /// <summary>
        /// Determines if two ValueObjects are unequal
        /// </summary>
        /// <param name="left">The left ValueObject in the comparison</param>
        /// <param name="right">The right ValueObject in the comparison</param>
        /// <returns></returns>
        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !(EqualOperator(left, right));
        }
        /// <summary>
        /// Enumerates the ValueObject's properties as a collection.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<object> GetAtomicValues();
        /// <summary>
        /// Determines equality
        /// </summary>
        /// <param name="obj">The object being compared.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject)obj;
            var thisValues = GetAtomicValues().GetEnumerator();
            var otherValues = other.GetAtomicValues().GetEnumerator();

            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (thisValues.Current is null ^ otherValues.Current is null)
                {
                    return false;
                }

                if (thisValues.Current != null &&
                    !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }

            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }
        /// <summary>
        /// Gets the Hash Code.
        /// </summary>
        /// <returns>An integer HashCode.</returns>
        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}

