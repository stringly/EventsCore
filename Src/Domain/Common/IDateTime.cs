﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EventsCore.Domain.Common
{
    public interface IDateTime    
    {       
        DateTime Now { get; }
    }
}