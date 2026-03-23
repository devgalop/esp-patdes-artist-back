using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace culturalEvents.Shared.Abstractions
{
    /// <summary>
    /// Marker interface for commands.
    /// </summary>
    public interface ICommand{}

    /// <summary>
    /// Interface for handling commands. It takes a command of type TCommand and returns a Task.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// Handles the command asynchronously.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task HandleAsync(TCommand command);
    }
}