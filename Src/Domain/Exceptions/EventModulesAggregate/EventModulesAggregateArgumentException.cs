using System;
using System.Collections.Generic;
using System.Text;

namespace EventsCore.Domain.Exceptions.EventModulesAggregate
{
    public class EventModulesAggregateArgumentException : ArgumentException
    {
        public EventModulesAggregateArgumentException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
