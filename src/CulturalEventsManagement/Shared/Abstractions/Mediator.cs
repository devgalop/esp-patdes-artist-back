namespace CulturalEventsManagement.Shared.Abstractions;

public interface IMediator
{
    /// <summary>
    /// Envia la consulta al handler correspondiente y devuelve el resultado.
    /// </summary>
    /// <typeparam name="TQuery">Tipo de Query</typeparam>
    /// <typeparam name="TResult">Tipo de Resultado</typeparam>
    /// <param name="query">Query</param>
    /// <returns></returns>
    Task<TResult> SendAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery;

    /// <summary>
    /// Envía el comando al handler correspondiente.
    /// </summary>
    /// <typeparam name="TCommand">Tipo de comando</typeparam>
    /// <param name="command">Comando</param>
    /// <returns></returns>
    Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
}

public class Mediator(IServiceProvider serviceProvider) : IMediator
{
    /// <summary>
    /// Envía la consulta al handler correspondiente y devuelve el resultado.
    /// </summary>
    /// <typeparam name="TQuery">Tipo de Query</typeparam>
    /// <typeparam name="TResult">Tipo de Resultado</typeparam>
    /// <param name="query">Query</param>
    /// <returns>Resultado de la consulta</returns>
    public async Task<TResult> SendAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery
    {
        var handler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return await handler.HandleAsync(query);
    }

    /// <summary>
    /// Envía el comando al handler correspondiente.
    /// </summary>
    /// <typeparam name="TCommand">Tipo de comando</typeparam>
    /// <param name="command">Comando</param>
    /// <returns>Tarea que representa la operación asincrónica</returns>
    public async Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
    {
        var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.HandleAsync(command);
    }
}

public static class MediatorExtensions
{
    /// <summary>
    /// Registra el mediador en los servicios de la aplicación.
    /// </summary>
    /// <param name="builder">builder de aplicación</param>
    /// <returns></returns>
    public static WebApplicationBuilder AddMediator(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IMediator, Mediator>();
        return builder;
    }
}
