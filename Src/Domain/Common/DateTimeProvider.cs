using System;

namespace EventsCore.Domain.Common
{
    public class DateTimeProvider : IDateTime
    {
        public DateTime Now {
            get {
                return System.DateTime.Now;
            }
        }
    }
}
