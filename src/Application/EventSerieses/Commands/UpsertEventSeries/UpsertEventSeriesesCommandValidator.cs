using FluentValidation;

namespace EventsCore.Application.EventSerieses.Commands.UpsertEventSeries
{
    /// <summary>
    /// Implemenation of <see cref="AbstractValidator{T}"></see> used in the <see cref="UpsertEventSeriesesCommand"/>
    /// </summary>
    public class UpsertEventSeriesesCommandValidator : AbstractValidator<UpsertEventSeriesesCommand>
    {
        /// <summary>
        /// Creates a new instance of the Validator
        /// </summary>
        public UpsertEventSeriesesCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title).MaximumLength(25).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
