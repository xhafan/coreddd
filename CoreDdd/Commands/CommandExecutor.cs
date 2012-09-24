using System;
using CoreDdd.Infrastructure;

namespace CoreDdd.Commands
{
    public class CommandExecutor : ICommandExecutor
    {
        public void Execute<TCommandMessage>(TCommandMessage commandMessage) where TCommandMessage : ICommand
        {
            var handler = IoC.Resolve<ICommandHandler<TCommandMessage>>();
            handler.CommandExecuted += CommandExecuted;
            handler.Execute(commandMessage);
        }

        public event EventHandler<CommandExecutedArgs> CommandExecuted;
    }
}