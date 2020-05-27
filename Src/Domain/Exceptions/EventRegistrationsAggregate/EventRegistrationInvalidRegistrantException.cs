using System;
using System.Collections.Generic;
using System.Text;

namespace EventsCore.Domain.Exceptions.EventRegistrationsAggregate
{
    public class EventRegistrationAggregateInvalidRegistrantException : ArgumentNullException
    {
        public EventRegistrationAggregateInvalidRegistrantException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
