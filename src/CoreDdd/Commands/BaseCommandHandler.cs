using System;

namespace CoreDdd.Commands
{
    public abstract class BaseCommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public abstract void Execute(TCommand message);
        public event EventHandler<CommandExecutedArgs> CommandExecuted;

        protected void RaiseCommandExecutedEvent(CommandExecutedArgs eventArgs)
        {
            CommandExecuted?.Invoke(this, eventArgs);
        }
    }
}