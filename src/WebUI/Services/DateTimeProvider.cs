using EventsCore.Common;
using System;

namespace EventsCore.WebUI.Services
{
    public class DateTimeProvider : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
