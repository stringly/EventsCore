using EventsCore.Domain.Entities;
using MediatR;

namespace EventsCore.Application.EventTypes.Commands.UpsertEventType
{
    /// <summary>
    /// Implementation of <see cref="IRequest"></see> that creates/updates a <see cref="EventType"></see>
    /// </summary>
    public class UpsertEventTypeCommand : IRequest<int>
    {
        /// <summary>
        /// The Id of the <see cref="EventType"></see> being upserted
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// The Name of the <see cref="EventType"></see> being upserted.
        /// </summary>
        public string Name { get; set; }


    }
}
