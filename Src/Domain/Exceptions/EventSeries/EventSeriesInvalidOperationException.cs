using System;

namespace EventsCore.Domain.Exceptions.EventSeries
{
    public class EventSeriesInvalidOperationException : InvalidOperationException
    {
        public EventSeriesInvalidOperationException(string message) : base(message: message) { }
    }
}
