using CulturalEventsManagement.Shared.Abstractions;
using FluentValidation;

namespace CulturalEventsManagement.Modules.EventManagement.RescheduledEvent;

public record RescheduledEventRequest(
    string EventId,
    DateTime NewDate
): IQuery;

public sealed class RescheduledEventRequestValidator : AbstractValidator<RescheduledEventRequest>
{
    public RescheduledEventRequestValidator()
    {
        RuleFor(x => x.EventId).NotEmpty().WithMessage("El ID del evento es requerido.");
        RuleFor(x => x.NewDate).GreaterThan(DateTime.Now).WithMessage("La nueva fecha debe ser futura.");
    }
}
