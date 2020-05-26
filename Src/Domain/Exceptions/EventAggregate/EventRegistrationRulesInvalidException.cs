using System;
using System.Collections.Generic;
using System.Text;

namespace EventsCore.Domain.Exceptions.EventAggregate
{
    class EventRegistrationRulesInvalidException : ArgumentException
    {
        public EventRegistrationRulesInvalidException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
