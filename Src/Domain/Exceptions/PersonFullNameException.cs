using System;
using System.Collections.Generic;
using System.Text;

namespace EventsCore.Domain.Exceptions
{
    public class PersonFullNameException : Exception
    {
        public PersonFullNameException(string message) : base(message) { }
    }
}
