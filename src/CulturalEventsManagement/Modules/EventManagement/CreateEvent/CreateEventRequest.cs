using CulturalEventsManagement.Shared.Abstractions;
using CulturalEventsManagement.Shared.Domain;
using FluentValidation;

namespace CulturalEventsManagement.Modules.EventManagement.CreateEvent;

public sealed record CreateEventRequest
(
    string Name,
    string Description,
    DateTime ScheduledAt,
    EventDetails Details
): ICommand;

public sealed record EventDetails(
    string Type,
    string BillingType,
    decimal Price,
    string Location
);

public sealed class CreateEventRequestValidator : AbstractValidator<CreateEventRequest>
{
    public CreateEventRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("El nombre del evento es obligatorio.")
            .MaximumLength(150)
            .WithMessage("El nombre del evento no puede exceder los 150 caracteres.")
            .MinimumLength(5)
            .WithMessage("El nombre del evento debe tener al menos 5 caracteres.");

        RuleFor(x => x.Description)
            .MaximumLength(255)
            .WithMessage("La descripción del evento no puede exceder los 255 caracteres.");

        RuleFor(x => x.ScheduledAt)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("La fecha programada debe ser en el futuro.");
    }
}

public sealed class EventDetailsValidator : AbstractValidator<EventDetails>
{
    public EventDetailsValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty()
            .WithMessage("El tipo del evento es obligatorio.")
            .Must(type => Enum.TryParse<EventType>(type, true, out _))
            .WithMessage("El tipo del evento no es válido.");

        RuleFor(x => x.BillingType)
            .NotEmpty()
            .WithMessage("El tipo de facturación del evento es obligatorio.")
            .Must(billingType => Enum.TryParse<EventBillingType>(billingType, true, out _))
            .WithMessage("El tipo de facturación del evento no es válido.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("El precio del evento debe ser mayor o igual a 0.");
    }
}


