using System;
using System.Collections.Generic;
using System.Text;

namespace EventsCore.Domain.Exceptions.EventAggregate
{
    public class EventRegistrationInvalidRegistrantException : ArgumentNullException
    {
        public EventRegistrationInvalidRegistrantException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
