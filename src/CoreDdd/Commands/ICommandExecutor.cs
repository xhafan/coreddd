using System;

namespace CoreDdd.Commands
{
    public interface ICommandExecutor
    {
        void Execute<TCommand>(TCommand command) where TCommand : ICommand;
        event EventHandler<CommandExecutedArgs> CommandExecuted;    
    }
}
