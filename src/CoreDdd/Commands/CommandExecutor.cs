using System;
#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Commands
{
    /// <summary>
    /// Instantiates a command handler for a given command, and executes it.
    /// </summary>
    public class CommandExecutor : ICommandExecutor
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="commandHandlerFactory">A command handler factory</param>
        public CommandExecutor(ICommandHandlerFactory commandHandlerFactory)
        {
            _commandHandlerFactory = commandHandlerFactory;
        }

        /// <summary>
        /// Executes a command handler based on the command type.
        /// </summary>
        /// <typeparam name="TCommand">A command type</typeparam>
        /// <param name="command">An instance of a command with data</param>
        public void Execute<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandHandler = _commandHandlerFactory.Create<TCommand>();

            try
            {
                commandHandler.CommandExecuted += CommandExecuted;
                commandHandler.Execute(command);
            }
            finally
            {
                _commandHandlerFactory.Release(commandHandler);
            }
        }

#if !NET40
        /// <summary>
        /// Executes a command handler asynchronously based on the command type.
        /// </summary>
        /// <typeparam name="TCommand">A command type</typeparam>
        /// <param name="command">An instance of a command with data</param>
        public async Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandHandler = _commandHandlerFactory.Create<TCommand>();

            try
            {
                commandHandler.CommandExecuted += CommandExecuted;
                await commandHandler.ExecuteAsync(command).ConfigureAwait(false);
            }
            finally
            {
                _commandHandlerFactory.Release(commandHandler);
            }
        }
#endif

        /// <summary>
        /// Command executed event, passed down into a command handler, and optionally raised by a command handler.
        /// </summary>
        public event Action<CommandExecutedArgs>? CommandExecuted;
    }
}