using System;
using System.Collections.Generic;
using System.Text;

namespace EventsCore.Domain.Exceptions
{
    public class AddressInvalidException : Exception
    {
        public AddressInvalidException(string message) : base(message) { }
    }
}
