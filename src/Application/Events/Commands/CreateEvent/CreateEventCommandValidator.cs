using EventsCore.Domain.Common;
using FluentValidation;
using System.Security.Cryptography.X509Certificates;

namespace EventsCore.Application.Events.Commands.CreateEvent
{
    /// <summary>
    /// Implemenation of <see cref="AbstractValidator{T}"></see> used in the <see cref="CreateEventCommand"/>
    /// </summary>
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        private readonly IDateTime _dateTime;
        /// <summary>
        /// Creates a new instance of the Validator
        /// </summary>
        /// <param name="dateTime">An implementation of <see cref="IDateTime"></see></param>
        public CreateEventCommandValidator(IDateTime dateTime)
        {
            _dateTime = dateTime;
            RuleFor(x => x.Title).NotEmpty()
                .WithMessage("An Event Title is required.")
                .MaximumLength(50)
                .WithMessage("Maximum length for Event Title is 50 characters.");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("An Event Description is required.");
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
            RuleFor(x => x.EventTypeId)
                .NotEmpty()
                .WithMessage("An Event Type is required.");
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
