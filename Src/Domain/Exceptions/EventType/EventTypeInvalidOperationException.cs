using System;

namespace EventsCore.Domain.Exceptions.EventType
{
    public class EventTypeInvalidOperationException : InvalidOperationException
    {
        public EventTypeInvalidOperationException(string message) : base(message: message) { }
    }
}
