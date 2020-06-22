using MediatR;

namespace EventsCore.Application.Events.Queries.GetUpcomingEvents
{
    /// <summary>
    /// Implementation of <see cref="IRequest"></see> that returns a list of <see cref="UpcomingEventDto"></see> in an <see cref="UpcomingEventListVm"/>
    /// </summary>
    /// <remarks>
    /// This query will return events that meet the following criteria:
    /// <list type="bullet">
    /// <item><description>Have a start date within 60 days of the current system time</description></item>
    /// <item><description>Do not have a registration for the current user.</description></item>
    /// </list>
    /// </remarks>
    public class GetUpcomingEventsQuery : IRequest<UpcomingEventListVm>
    {
    }
}
