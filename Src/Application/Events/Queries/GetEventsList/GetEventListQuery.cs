using MediatR;

namespace EventsCore.Application.Events.Queries.GetEventsList
{
    /// <summary>
    /// Implementation of <see cref="IRequest"></see> that returns a list of <see cref="EventDto"></see> in an <see cref="EventListVm"/>
    /// </summary>
    public class GetEventListQuery : IRequest<EventListVm>
    {
        // TODO: can I use a specification here?
        public int? EventTypeId { get; set; } = null;
        public string SearchString { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 25;
        public string CurrentSort { get; set; }
    }
}
