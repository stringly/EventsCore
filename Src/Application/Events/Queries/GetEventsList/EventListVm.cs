using System.Collections.Generic;

namespace EventsCore.Application.Events.Queries.GetEventsList
{
    /// <summary>
    /// Class that serves as a Viewmodel used in the <see cref="GetEventListQuery"/>
    /// </summary>
    public class EventListVm
    {
        /// <summary>
        /// A list of <see cref="EventDto"/> objects.
        /// </summary>
        public IList<EventDto> Events { get; set; }
        /// <summary>
        /// The count of entities in the list
        /// </summary>
        public int Count { get; set; }
    }
}
