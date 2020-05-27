using System;

namespace EventsCore.Domain.Exceptions.Event
{
    public class EventArgumentException : ArgumentException
    {
        public EventArgumentException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
