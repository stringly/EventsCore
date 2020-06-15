using MediatR;

namespace EventsCore.Application.Events.Commands.DeleteEvent
{
    /// <summary>
    /// An implementation of <see cref="IRequest"></see> that handles a request to remove an <see cref="Domain.Entities.Event"></see>
    /// </summary>
    public class DeleteEventCommand : IRequest
    {
        /// <summary>
        /// The Id of the Event to be removed.
        /// </summary>
        public int Id { get; set; }
    }
}
