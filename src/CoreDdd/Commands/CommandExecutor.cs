using System;
using CoreIoC;
#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Commands
{
    public class CommandExecutor : ICommandExecutor
    {
        public void Execute<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandHandler = IoC.Resolve<ICommandHandler<TCommand>>();

            try
            {
                commandHandler.CommandExecuted += CommandExecuted;
                commandHandler.Execute(command);
            }
            finally
            {
                IoC.Release(commandHandler);
            }
        }

#if !NET40
        public async Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandHandler = IoC.Resolve<ICommandHandler<TCommand>>();

            try
            {
                commandHandler.CommandExecuted += CommandExecuted;
                await commandHandler.ExecuteAsync(command);
            }
            finally
            {
                IoC.Release(commandHandler);
            }
        }
#endif

        public event Action<CommandExecutedArgs> CommandExecuted;
    }
}