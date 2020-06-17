using MediatR;

namespace EventsCore.Application.Events.Queries.GetEventDetail
{
    /// <summary>
    /// Implementation of <see cref="IRequest"></see> that returns a list of <see cref="EventDetailVm"></see>
    /// </summary>
    public class GetEventDetailQuery : IRequest<EventDetailVm>
    {
        /// <summary>
        /// The Id of the Entity
        /// </summary>
        public int Id { get;set;}
    }
}
