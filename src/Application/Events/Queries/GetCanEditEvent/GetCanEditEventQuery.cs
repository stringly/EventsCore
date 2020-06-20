using MediatR;

namespace EventsCore.Application.Events.Queries.GetCanEditEvent
{
    /// <summary>
    /// Implementation of <see cref="IRequest"></see> that returns whether the current user can edit/delete an event.
    /// </summary>
    public class GetCanEditEventQuery : IRequest<bool>
    {
        /// <summary>
        /// The Id of the Event.
        /// </summary>
        public int EventId { get;set;}
    }
}
