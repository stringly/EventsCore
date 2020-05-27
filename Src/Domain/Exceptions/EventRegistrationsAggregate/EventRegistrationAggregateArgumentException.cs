using System;

namespace EventsCore.Domain.Exceptions.EventRegistrationsAggregate
{
    public class EventRegistrationAggregateArgumentException : ArgumentException
    {
        public EventRegistrationAggregateArgumentException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
