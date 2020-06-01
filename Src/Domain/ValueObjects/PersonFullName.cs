using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.ValueObjects;
using System.Collections.Generic;

namespace EventsCore.Domain.ValueObjects
{
    /// <summary>
    /// Value object that stores a person's First and Last Name
    /// </summary>    
    public class PersonFullName : ValueObject
    {
        /// <summary>
        /// Private, parameterless constructor implemented for EF. This object cannot be created with no parameters
        /// </summary>
        private PersonFullName() { }

        /// <summary>
        /// Constructor for the PersonFullName object.
        /// </summary>
        /// <param name="first">A string containing the Person's First or Given Name. Required.</param>
        /// <param name="last">A string containing the Person's Last or Surname. Required.</param>
        /// <exception cref="PersonFullNameException">
        /// Thrown when:
        /// <list type="bullet">
        /// <item><description>The first parameter is null/whitespace</description></item>
        /// <item><description>The last parameter is null/whitespace</description></item>
        /// </list>
        /// </exception>
        public PersonFullName(string first, string last)
        {
            if (string.IsNullOrWhiteSpace(first))
            {
                throw new PersonFullNameException("Cannot set Person First name to empty string.");
            }
            else if (string.IsNullOrWhiteSpace(last))
            {
                throw new PersonFullNameException("Cannot set Person Last name to empty string.");
            }
            First = first;
            Last = last;
        }
        /// <summary>
        /// Person's First/Given Name
        /// </summary>
        public string First { get; private set; }

        /// <summary>
        /// Person's Last/Surname
        /// </summary>
        public string Last { get; private set; }

        /// <summary>
        /// Returns the person's full name in the format "First Last"
        /// </summary>
        public string FullName => First + " " + Last;

        /// <summary>
        /// Returns the person's full name in the format "Last, First"
        /// </summary>
        public string FullNameReverse => $"{Last}, {First}";
        /// <summary>
        /// Enumerates the values in the object
        /// </summary>
        /// <returns>An <see cref="System.Collections.IEnumerable"></see> containing the values in the object.</returns>
        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return First;
            yield return Last;
        }
    }
}
