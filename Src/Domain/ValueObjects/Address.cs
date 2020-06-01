using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.ValueObjects;
using System;
using System.Collections.Generic;

namespace EventsCore.Domain.ValueObjects
{
    /// <summary>
    ///  Value object that stores an Address
    /// </summary>
    public class Address : ValueObject
    {
        /// <summary>
        /// The Street Address, eg "123 Anywhere St."
        /// </summary>
        public String Street { get; private set; }
        /// <summary>
        /// The Suite/Apartment/Room Number, optional field
        /// </summary>
        public String Suite { get; private set; }
        /// <summary>
        /// The City Name
        /// </summary>
        public String City { get; private set; }        
        /// <summary>
        /// The Postal Abbrevation for the State
        /// </summary>
        public String State { get; private set; }
        /// <summary>
        /// The 5-digit ZIP Code
        /// </summary>
        public String ZipCode { get; private set; }
        /// <summary>
        /// Private, parameterless constructor implemented for EF purposes. This object cannot be created with no parameters.
        /// </summary>
        private Address() { }
        /// <summary>
        /// Creates a new Address instance from the provided parameters.
        /// </summary>
        /// <param name="street">The street address, e.g. "123 Anywhere St." Required, cannot be null/whitespace/empty string.</param>
        /// <param name="suite">The suite/apartment/room number. This is an optional field.</param>
        /// <param name="city">The name of the city in which the address is located. Required, cannot be null/whitespace/empty string.</param>
        /// <param name="state">The 2-digit Postal Abbreviation for the state in which the address is located. Required, cannot be null/whitespace/empty string.</param>        
        /// <param name="zipCode">The 5-digit ZIP code for the address. Required, cannot be null/whitespace/empty string.</param>
        /// <exception cref="AddressInvalidException">
        /// Thrown when:
        /// <list type="bullet">
        /// <item><description>The street parameter is null/whitespace</description></item>
        /// <item><description>The city parameter is null/whitespace</description></item>
        /// <item><description>The state parameter is null/whitespace</description></item>
        /// <item><description>The zipCode parameter is null/whitespace</description></item>
        /// </list>
        /// </exception>
        public Address(string street, string suite, string city, string state, string zipCode)
        {
            if (string.IsNullOrWhiteSpace(street))
            {
                throw new AddressInvalidException("Street address must not be empty/null string.");
            }
            else if (string.IsNullOrWhiteSpace(city))
            {
                throw new AddressInvalidException("City must not be empty/null string.");
            }
            else if (string.IsNullOrWhiteSpace(state))
            {
                throw new AddressInvalidException("State must not be empty/null string.");
            }
            else if (string.IsNullOrWhiteSpace(zipCode))
            {
                throw new AddressInvalidException("Zip Code must not be empty/null string.");
            }

            Street = street;
            Suite = suite;
            City = city;
            State = state;            
            ZipCode = zipCode;
        }
        /// <summary>
        /// Enumerates the Values in the object.
        /// </summary>
        /// <returns>An <see cref="System.Collections.IEnumerable"/> containing the values in the object.</returns>
        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Street;
            yield return Suite;
            yield return City;
            yield return State;
            yield return ZipCode;
        }
    }
}
