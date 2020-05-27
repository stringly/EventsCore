using System;

namespace EventsCore.Domain.Exceptions.Event
{
    public class EventInvalidOperationException : InvalidOperationException
    {
        public EventInvalidOperationException(string message) : base(message: message) { }

    }
}
