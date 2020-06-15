using System;
using System.Collections.Generic;
using System.Text;

namespace EventsCore.Application.Events.Queries.GetEventDetail
{
    public class EventDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set;  }
        public int EventTypeId { get; set; }
        public string EventTypeTitle { get; set; }
        public string EventPeriod { get; set; }        
        public string RegistrationPeriod { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

    }
}
