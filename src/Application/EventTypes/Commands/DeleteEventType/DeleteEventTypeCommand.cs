using EventsCore.Domain.Entities;
using MediatR;

namespace EventsCore.Application.EventTypes.Commands.DeleteEventType
{
    /// <summary>
    /// An implementation of <see cref="IRequest"></see> that handles a request to remove an <see cref="EventType"></see>
    /// </summary>
    public class DeleteEventTypeCommand : IRequest
    {
        /// <summary>
        /// The Id of the EventType to be removed
        /// </summary>
        public int Id { get; set; }
    }
}
