using System;
using CoreIoC;

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

        public event Action<CommandExecutedArgs> CommandExecuted;
    }
}