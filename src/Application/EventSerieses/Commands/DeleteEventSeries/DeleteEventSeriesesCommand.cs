using EventsCore.Domain.Entities;
using MediatR;

namespace EventsCore.Application.EventSerieses.Commands.DeleteEventSeries
{
    /// <summary>
    /// An implementation of <see cref="IRequest"></see> that handles a request to remove a <see cref="EventSeries"></see>
    /// </summary>
    public class DeleteEventSeriesesCommand : IRequest
    {
        /// <summary>
        /// The Id of the Event Series to be removed.
        /// </summary>
        public int Id { get; set; }
    }
}
