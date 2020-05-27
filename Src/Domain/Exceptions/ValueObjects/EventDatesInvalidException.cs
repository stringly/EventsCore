using System;

namespace EventsCore.Domain.Exceptions.ValueObjects
{
    public class EventDatesInvalidException : Exception
    {
        public EventDatesInvalidException(string message) : base(message: message) { } 
    }
}
