using EventsCore.Domain.Common;
using FluentValidation;

namespace EventsCore.Application.Events.Commands.UpdateEvent
{
    /// <summary>
    /// Implementation of <seealso cref="AbstractValidator{T}"/> used to validate data in the <see cref="UpdateEventCommand"></see>
    /// </summary>
    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        private readonly IDateTime _dateTime;
        /// <summary>
        /// Creates a new instance of the validator
        /// </summary>
        /// <param name="dateTime">An implementation of <see cref="IDateTime"></see> used to obtain the system time.</param>
        public UpdateEventCommandValidator(IDateTime dateTime)
        {
            _dateTime = dateTime;
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Event Id is required.")
                .GreaterThan(0)
                .WithMessage("Event Id cannot be 0.");
            RuleFor(x => x.Title).NotEmpty()
                .WithMessage("An Event Title is required.")
                .MaximumLength(50)
                .WithMessage("Maximum length for Event Title is 50 characters.");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("An Event Description is required.");
            RuleFor(x => x.EventTypeId)
                .NotEmpty()
                .WithMessage("An Event Type is required.")
                .GreaterThan(0)
                .WithMessage("A valid Event Type is required.");
            RuleFor(x => x.StartDate)
                .NotEmpty()
                .GreaterThan(_dateTime.Now)
                .WithMessage("Event Start Date must be in the future.");
            RuleFor(x => x.EndDate)
                .NotEmpty()
                .GreaterThan(x => x.StartDate)
                .WithMessage("Event End Date must be after Event Start Date.");
            RuleFor(x => x.RegEndDate)
                .NotEmpty()
                .LessThanOrEqualTo(x => x.StartDate)
                .WithMessage("Registration Period End Date cannot be greater than Event Start Date");
            RuleFor(x => x.RegStartDate)
                .NotEmpty()
                .LessThan(x => x.RegEndDate)
                .WithMessage("Registration Period Start Date must be before Registration Period End Date.");
            RuleFor(x => x.MaxRegsCount)
                .NotEmpty()
                .WithMessage("Maximum Attendees is required.");
            RuleFor(x => x.MaxRegsCount).GreaterThan(0);
            RuleFor(x => x.MinRegsCount)
                .LessThanOrEqualTo(x => x.MaxRegsCount)
                .When(x => x.MinRegsCount.HasValue)
                .WithMessage("The Minimum Attendess count must be less than or equal to the Maximum Attendees count");
            RuleFor(x => x.MaxStandbyCount)
                .GreaterThan(0)
                .When(x => x.MaxStandbyCount.HasValue)
                .WithMessage("Maximum standby count must be greater than zero to enable standby registrations for this event.");
            RuleFor(x => x.EventSeriesId)
                .GreaterThan(0)
                .When(x => x.EventSeriesId.HasValue);
            RuleFor(x => x.Street)
                .MaximumLength(50)
                .When(x => !string.IsNullOrEmpty(x.Street));
            RuleFor(x => x.Suite)
                .MaximumLength(20)
                .When(x => !string.IsNullOrEmpty(x.Suite));
            RuleFor(x => x.City)
                .MaximumLength(50)
                .When(x => !string.IsNullOrEmpty(x.City));
            RuleFor(x => x.State)
                .MaximumLength(2)
                .When(x => !string.IsNullOrEmpty(x.State));
            RuleFor(x => x.Zip)
                .Matches(@"^\d{5}(?:[-\s]\d{4})?$")
                .When(x => !string.IsNullOrEmpty(x.Zip));

        }

    }
}
