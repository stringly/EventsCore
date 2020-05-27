using System;

namespace EventsCore.Domain.Exceptions.ValueObjects
{
    public class PersonFullNameException : Exception
    {
        public PersonFullNameException(string message) : base(message) { }
    }
}
