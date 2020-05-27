using System;

namespace EventsCore.Domain.Exceptions.EventRegistrationsAggregate
{
    public class EventRegistrationAggregateInvalidOperationException : InvalidOperationException
    {
        public EventRegistrationAggregateInvalidOperationException(string message) : base(message: message) { }
    }
}
