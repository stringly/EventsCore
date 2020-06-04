using FluentValidation;

namespace EventsCore.Application.Ranks.Commands.UpsertRank
{
    /// <summary>
    /// Implemenation of <see cref="AbstractValidator{T}"></see> used in the <see cref="UpsertRankCommand"/>
    /// </summary>
    public class UpsertRankCommandValidator : AbstractValidator<UpsertRankCommand>
    {
        /// <summary>
        /// Creates a new instance of the Validator
        /// </summary>
        public UpsertRankCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Abbrev)
                .NotEmpty()
                .MaximumLength(10);
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(10);
        }
    }
}
