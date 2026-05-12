using CulturalEventsManagement.Shared.Abstractions;
using FluentValidation;

namespace CulturalEventsManagement.Modules.OrderManagement.CreateOrder;

public sealed record CreateOrderRequest(
    string UserId,
    string EventId,
    string ProviderId,
    List<OrderDetails> Details,
    string? CuponCode,
    string? Observations
):IQuery;

public sealed record OrderDetails(
    string ProductId,
    int Quantity
);

public sealed class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.")
            .MaximumLength(100)
            .WithMessage("UserId must not exceed 100 characters.")
            .MinimumLength(5)
            .WithMessage("UserId must be at least 5 characters long.");
        RuleFor(x => x.EventId)
            .NotEmpty()
            .WithMessage("EventId is required.")
            .MaximumLength(100)
            .WithMessage("EventId must not exceed 100 characters.")
            .MinimumLength(5)
            .WithMessage("EventId must be at least 5 characters long.");
        RuleFor(x => x.ProviderId)
            .NotEmpty()
            .WithMessage("ProviderId is required.")
            .MaximumLength(100)
            .WithMessage("ProviderId must not exceed 100 characters.")
            .MinimumLength(5)
            .WithMessage("ProviderId must be at least 5 characters long.");
        RuleFor(x => x.Details)
            .NotNull()
            .WithMessage("Order details are required.")
            .ForEach(x => x.SetValidator(new OrderDetailsValidator()));
    }

    public sealed class OrderDetailsValidator : AbstractValidator<OrderDetails>
    {
        public OrderDetailsValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("ProductId is required.")
                .MaximumLength(100)
                .WithMessage("ProductId must not exceed 100 characters.");
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.")
                .LessThanOrEqualTo(500)
                .WithMessage("Quantity must be less than or equal to 500.");
        }
    }
}
