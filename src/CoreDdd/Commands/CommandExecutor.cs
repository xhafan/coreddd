using System;
#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Commands
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;

        public CommandExecutor(ICommandHandlerFactory commandHandlerFactory)
        {
            _commandHandlerFactory = commandHandlerFactory;
        }

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

        public event Action<CommandExecutedArgs> CommandExecuted;
    }
}