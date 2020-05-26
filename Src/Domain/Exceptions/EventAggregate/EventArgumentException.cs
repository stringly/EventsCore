using System;
using System.Collections.Generic;
using System.Text;

namespace EventsCore.Domain.Exceptions.EventAggregate
{
    class EventArgumentException : ArgumentException
    {
        public EventArgumentException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
