using FluentValidation;

namespace EventsCore.Application.EventTypes.Commands.UpsertEventType
{
    /// <summary>
    /// Implemenation of <see cref="AbstractValidator{T}"></see> used in the <see cref="UpsertEventTypeCommand"/>
    /// </summary>
    public class UpsertEventTypeCommandValidator : AbstractValidator<UpsertEventTypeCommand>
    {
        /// <summary>
        /// Creates a new instance of the Validator
        /// </summary>
        public UpsertEventTypeCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).MaximumLength(25).NotEmpty();
        }
    }
}
