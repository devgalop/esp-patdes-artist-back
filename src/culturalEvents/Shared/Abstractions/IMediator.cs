using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace culturalEvents.Shared.Abstractions
{
    public interface IMediator
    {
        /// <summary>
        /// Sends a command of type TCommand to the appropriate handler and returns a Task representing the asynchronous operation.
        /// </summary>
        /// <typeparam name="TCommand">The type of the command.</typeparam>
        /// <param name="command">The command to send.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
        
        /// <summary>
        /// Sends a query of type TQuery to the appropriate handler and returns the result of type TResult.
        /// </summary>
        /// <typeparam name="TQuery">The type of the query.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="query">The query to send.</param>
        /// <returns>The result of the query.</returns>
        Task<TResult> SendAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery;
    }

    public class Mediator(IServiceProvider serviceProvider) : IMediator
    {
        public async Task<TResult> SendAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            var handler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
            return await handler.HandleAsync(query);
        }

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
}