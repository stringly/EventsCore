using EventsCore.Domain.Common;
using System;
namespace EventsCore.Domain.UnitTests.Common
{
    public class DateTimeTestProvider : IDateTime
    {
        public DateTime Now { get { return new DateTime(2020, 1, 1); } }
    }
}
