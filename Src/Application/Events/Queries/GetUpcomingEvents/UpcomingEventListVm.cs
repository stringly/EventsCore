using System.Collections.Generic;

namespace EventsCore.Application.Events.Queries.GetUpcomingEvents
{
    /// <summary>
    /// Class that serves as a viewmodel result for the <see cref="GetUpcomingEventsQuery"/>
    /// </summary>
    public class UpcomingEventListVm
    {
        /// <summary>
        /// A list of <see cref="UpcomingEventDto"/>
        /// </summary>
        public IList<UpcomingEventDto> Events { get; set; }
        /// <summary>
        /// The count of items in the list.
        /// </summary>
        public int Count { get; set; }
    }
}
