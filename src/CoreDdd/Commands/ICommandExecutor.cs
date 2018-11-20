using System;
#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Commands
{
    /// <summary>
    /// Executes a command handling logic for a given command.
    /// </summary>
    public interface ICommandExecutor
    {
        /// <summary>
        /// Executes a command handler for a given command.
        /// </summary>
        /// <typeparam name="TCommand">A command type</typeparam>
        /// <param name="command">An instance of command with data</param>
        void Execute<TCommand>(TCommand command) where TCommand : ICommand;
        
        /// <summary>
        /// Command executed event which is raised when handling the command.
        /// </summary>
        event Action<CommandExecutedArgs> CommandExecuted;

#if !NET40
        /// <summary>
        /// Executes a command handler asynchronously for a given command.
        /// </summary>
        /// <typeparam name="TCommand">A command type</typeparam>
        /// <param name="command">An instance of command with data</param>
        Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;
#endif
    }
}
