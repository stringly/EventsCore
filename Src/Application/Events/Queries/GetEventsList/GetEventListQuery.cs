using MediatR;

namespace EventsCore.Application.Events.Queries.GetEventsList
{
    /// <summary>
    /// Implementation of <see cref="IRequest"></see> that returns a list of <see cref="EventDto"></see> in an <see cref="EventListVm"/>
    /// </summary>
    public class GetEventListQuery : IRequest<EventListVm>
    {
        // TODO: can I use a specification here?
        /// <summary>
        /// The Id of the EventType to use to filter the results
        /// </summary>
        public int? EventTypeId { get; set; } = null;
        /// <summary>
        /// A string containing text to filter the results
        /// </summary>
        public string SearchString { get; set; }
        /// <summary>
        /// The Current Page
        /// </summary>
        public int CurrentPage { get; set; } = 1;
        /// <summary>
        /// The Number of items per page
        /// </summary>
        public int PageSize { get; set; } = 25;
    }
}
