﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EventsCore.Domain.Exceptions
{
    public class EventDatesInvalidException : Exception
    {
        public EventDatesInvalidException(string message) : base(message) { }
    }
}
