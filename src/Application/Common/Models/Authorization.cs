using EventsCore.Application.Common.Interfaces;
using EventsCore.Application.Events.Commands.DeleteEvent;
using EventsCore.Application.Events.Commands.UpdateEvent;
using EventsCore.Application.Events.Queries.GetCanEditEvent;
using EventsCore.Application.Events.Queries.GetEventEdit;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EventsCore.Application.Common.Models
{
    /// <summary>
    /// Assembly that contains Authorization handlers.
    /// </summary>
    public class Authorization
    {
        /// <summary>
        /// Implementatino of <see cref="IAuthorizer{GetEventEditQuery}"/> that validates authorization to edit an Event.
        /// </summary>
        public class CanGetEditEventAuthorizer : IAuthorizer<GetEventEditQuery>
        {
            private readonly IMediator _mediator;
            /// <summary>
            /// Creates a new instance of the validator.
            /// </summary>
            /// <param name="mediator">An implementation of <see cref="IMediator"/></param>
            public CanGetEditEventAuthorizer(IMediator mediator)
            {
                _mediator = mediator;
            }
            /// <summary>
            /// Performs the auth check.
            /// </summary>
            /// <param name="instance">The instance of the request command.</param>
            /// <param name="cancellationToken">A cancellationToken.</param>
            /// <returns></returns>
            public async Task<AuthorizationResult> AuthorizeAsync(GetEventEditQuery instance, CancellationToken cancellationToken = default)
            {
                bool result = await _mediator.Send(new GetCanEditEventQuery { EventId = instance.Id });
                if (result == true)
                {
                    return AuthorizationResult.Succeed();
                }
                return AuthorizationResult.Fail("You are not authorized to edit this event.");
            }
        }
        /// <summary>
        /// Implementatino of <see cref="IAuthorizer{UpdateEventCommand}"/> that validates authorization to edit an Event.
        /// </summary>
        public class CanEditEventAuthorizer : IAuthorizer<UpdateEventCommand>
        {
            private readonly IMediator _mediator;
            /// <summary>
            /// Creates a new instance of the validator.
            /// </summary>
            /// <param name="mediator">An implementation of <see cref="IMediator"/></param>
            public CanEditEventAuthorizer(IMediator mediator)
            {
                _mediator = mediator;
            }
            /// <summary>
            /// Performs the auth check.
            /// </summary>
            /// <param name="instance">The instance of the request command.</param>
            /// <param name="cancellationToken">A cancellationToken.</param>
            /// <returns></returns>
            public async Task<AuthorizationResult> AuthorizeAsync(UpdateEventCommand instance, CancellationToken cancellationToken = default)
            {
                bool result = await _mediator.Send(new GetCanEditEventQuery { EventId = instance.Id });
                if (result == true)
                {
                    return AuthorizationResult.Succeed();
                }
                return AuthorizationResult.Fail("You are not authorized to edit this event.");
            }
        }
        /// <summary>
        /// Implementatino of <see cref="IAuthorizer{UpdateEventCommand}"/> that validates authorization to delete an Event.
        /// </summary>
        public class CanDeleteEventAuthorizer : IAuthorizer<DeleteEventCommand>
        {
            private readonly IMediator _mediator;
            /// <summary>
            /// Creates a new instance of the validator
            /// </summary>
            /// <param name="mediator"></param>
            public CanDeleteEventAuthorizer(IMediator mediator)
            {
                _mediator = mediator;
            }
            /// <summary>
            /// Performs the auth check
            /// </summary>
            /// <param name="instance">An instance of the request command.</param>
            /// <param name="cancellationToken">A cancellationToken.</param>
            /// <returns></returns>
            public async Task<AuthorizationResult> AuthorizeAsync(DeleteEventCommand instance, CancellationToken cancellationToken = default)
            {
                bool result = await _mediator.Send(new GetCanEditEventQuery { EventId = instance.Id });
                if (result == true)
                {
                    return AuthorizationResult.Succeed();
                }
                return AuthorizationResult.Fail("You are not authorized to delete this event.");
            }
        }
    }
}
