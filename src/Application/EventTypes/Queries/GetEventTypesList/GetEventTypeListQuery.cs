using MediatR;

namespace EventsCore.Application.EventTypes.Queries.GetEventTypesList
{
    /// <summary>
    /// Implementation of <see cref="IRequest"></see> that returns a list of <see cref="EventTypeDto"></see> in an <see cref="EventTypeListVm"/>
    /// </summary>
    public class GetEventTypeListQuery : IRequest<EventTypeListVm>
    {
    }
}
