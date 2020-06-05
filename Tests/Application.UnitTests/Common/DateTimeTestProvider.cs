﻿using EventsCore.Domain.Common;
using System;
namespace EventsCore.Application.UnitTests.Common
{
    public class DateTimeTestProvider : IDateTime
    {
        public DateTimeTestProvider()
        {
            _date = new DateTime(2020, 1, 1);
        }
        public DateTimeTestProvider(DateTime date)
        {
            _date = date;
        }
        private readonly DateTime _date;
        public DateTime Now { get { return _date; } }
    }
}