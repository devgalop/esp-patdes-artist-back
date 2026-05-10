using CulturalEventsManagement.Shared.Abstractions;
using FluentValidation;

namespace CulturalEventsManagement.Modules.Marketplace.GetProviderCatalog;

public sealed record GetProviderCatalogRequest(
    string ProviderId
):IQuery;

public class GetProviderCatalogValidator : AbstractValidator<GetProviderCatalogRequest>
{
    public GetProviderCatalogValidator()
    {
        RuleFor(x => x.ProviderId)
            .NotEmpty().WithMessage("Provider id must not be empty.")
            .NotNull().WithMessage("Provider id must not be null.");
    }
}
