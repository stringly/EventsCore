using MediatR;

namespace EventsCore.Application.EventSerieses.Queries.GetEventSeriesesDetail
{
    /// <summary>
    /// Implementation of <see cref="IRequest"></see> that returns a list of <see cref="EventSeriesDetailVm"></see>
    /// </summary>
    public class GetEventSeriesDetailQuery : IRequest<EventSeriesDetailVm>
    {
        /// <summary>
        /// The Id of the Entity
        /// </summary>
        public int Id { get; set; }
    }
}
