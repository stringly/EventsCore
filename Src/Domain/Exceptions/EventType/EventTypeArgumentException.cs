using System;

namespace EventsCore.Domain.Exceptions.EventType
{
    public class EventTypeArgumentException : ArgumentException
    {
        public EventTypeArgumentException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
