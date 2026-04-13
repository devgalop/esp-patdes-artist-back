using FluentValidation;

namespace culturalEvents.Modules.EventManagement.CreateEvent;

public sealed class CreateEventValidator : AbstractValidator<CreateEventRequest>
{
    public CreateEventValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Event name is required.")
            .MaximumLength(100)
            .WithMessage("Event name must not exceed 100 characters.");

        RuleFor(x => x.UtcDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Event date must be in the future.");

        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Event category is required.")
            .MaximumLength(50)
            .WithMessage("Event category must not exceed 50 characters.");

        RuleFor(x => x.EventType)
            .NotEmpty()
            .WithMessage("Event type is required. (Paid or Free)")
            .MaximumLength(50)
            .WithMessage("Event type must not exceed 50 characters.");
    }
}
