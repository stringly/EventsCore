using System;

namespace EventsCore.Domain.Exceptions.ValueObjects
{
    public class AddressInvalidException : Exception
    {
        public AddressInvalidException(string message) : base(message) { }
    }
}
