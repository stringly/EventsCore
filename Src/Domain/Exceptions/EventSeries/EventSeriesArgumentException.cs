using System;

namespace EventsCore.Domain.Exceptions.EventSeries
{
    public class EventSeriesArgumentException : ArgumentException
    {
        public EventSeriesArgumentException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
