using System;
using System.Collections.Generic;
using System.Text;

namespace EventsCore.Domain.Exceptions.EventAggregate
{
    public class RegistrantInvalidException : ArgumentException
    {
        public RegistrantInvalidException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
