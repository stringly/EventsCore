using EventsCore.Domain.Entities;
using System.Collections.Generic;

namespace EventsCore.Application.EventSerieses.Queries.GetEventSeriesesList
{
    /// <summary>
    /// Class that serves as a Viewmodel for a list of <see cref="EventSeries"></see> entities
    /// </summary>
    public class EventSeriesesListVm 
    {
        /// <summary>
        /// A list of <see cref="EventSeriesDto"></see> objects.
        /// </summary>
        public IList<EventSeriesDto> EventSerieses { get; set; }

        /// <summary>
        /// The count of entities in the list
        /// </summary>
        public int Count { get; set; }
    }
}
