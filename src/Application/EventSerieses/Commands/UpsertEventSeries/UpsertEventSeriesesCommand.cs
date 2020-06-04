using EventsCore.Domain.Entities;
using MediatR;

namespace EventsCore.Application.EventSerieses.Commands.UpsertEventSeries
{
    /// <summary>
    /// Implementation of <see cref="IRequest"></see> that creates/updates a <see cref="EventSeries"/>
    /// </summary>
    public class UpsertEventSeriesesCommand : IRequest<int>
    {
        /// <summary>
        /// The Id of the <see cref="EventSeries"></see> being upserted. Will be null if an EvenSeries is being created.
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// The Name of the <see cref="EventSeries"></see> being upserted.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The description of the <see cref="EventSeries"></see> being upserted.
        /// </summary>
        public string Description { get; set; }
    }
}
