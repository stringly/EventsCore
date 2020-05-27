using System;

namespace EventsCore.Domain.Exceptions.ValueObjects
{
    public class EventRegistrationRulesArgumentException : ArgumentException
    {
        public EventRegistrationRulesArgumentException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
