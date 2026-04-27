namespace CulturalEventsManagement.Shared.Abstractions;

public interface IQuery{}

public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery
{
    /// <summary>
    /// Maneja la consulta y devuelve el resultado
    /// </summary>
    /// <param name="query">Query</param>
    /// <returns></returns>
    Task<TResult> HandleAsync(TQuery query);
}