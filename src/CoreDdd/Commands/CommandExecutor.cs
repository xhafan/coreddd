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
            commandHandler.CommandExecuted += CommandExecuted;
            commandHandler.Execute(command);
        }

#if !NET40
        public async Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandHandler = IoC.Resolve<ICommandHandler<TCommand>>();
            commandHandler.CommandExecuted += CommandExecuted;
            await commandHandler.ExecuteAsync(command);
        }
#endif

        public event Action<CommandExecutedArgs> CommandExecuted;
    }
}