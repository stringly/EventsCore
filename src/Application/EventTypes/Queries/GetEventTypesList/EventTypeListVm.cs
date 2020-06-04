using EventsCore.Domain.Entities;
using System.Collections.Generic;

namespace EventsCore.Application.EventTypes.Queries.GetEventTypesList
{
    /// <summary>
    /// Class that serves as a Viewmodel for a list of <see cref="EventType"></see> entities
    /// </summary>
    public class EventTypeListVm
    {
        /// <summary>
        /// A list of <see cref="EventTypeDto"></see> objects
        /// </summary>
        public IList<EventTypeDto> EventTypes { get; set; }
        /// <summary>
        /// The count of entities in the list.
        /// </summary>
        public int Count { get; set; }
    }
}
