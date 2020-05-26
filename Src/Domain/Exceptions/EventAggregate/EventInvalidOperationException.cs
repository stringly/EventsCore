using System;
using System.Collections.Generic;
using System.Text;

namespace EventsCore.Domain.Exceptions.EventAggregate
{
    class EventInvalidOperationException : InvalidOperationException
    {
        public EventInvalidOperationException(string message) : base(message: message) { }

    }
}
