using MediatR;

namespace EventsCore.Application.Events.Queries.GetEventEdit
{
    /// <summary>
    /// Implementation of <see cref="IRequest"></see> that returns a list of <see cref="EventEditDto"></see>
    /// </summary>
    public class GetEventEditQuery : IRequest<EventEditDto>
    {
        /// <summary>
        /// The Id of the Entity
        /// </summary>
        public int Id { get; set; }
    }
}
