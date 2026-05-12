namespace CulturalEventsManagement.Shared.Exceptions;

public sealed class InvalidProviderException(string providerId) : Exception($"Provider with id {providerId} does not exist or has no catalog.")
{

}

public sealed class ProductNotFoundException(string productId, string providerId) : Exception($"Product with id {productId} does not exist in provider {providerId}'s catalog.")
{

}

public sealed class CulturalEventNotFoundException(string eventId) : Exception($"Cultural event with id {eventId} was not found.")
{

}
