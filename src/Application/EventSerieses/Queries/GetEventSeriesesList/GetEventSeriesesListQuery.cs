﻿using MediatR;

namespace EventsCore.Application.EventSerieses.Queries.GetEventSeriesesList
{
    /// <summary>
    /// Implementation of <see cref="IRequest"></see> that returns a list of <see cref="EventSeriesDto"></see> in an <see cref="EventSeriesesListVm"/>
    /// </summary>
    public class GetEventSeriesesListQuery : IRequest<EventSeriesesListVm>
    {
    }
}
