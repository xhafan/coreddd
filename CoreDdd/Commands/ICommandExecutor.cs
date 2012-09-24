using System;

namespace CoreDdd.Commands
{
    public interface ICommandExecutor
    {
        void Execute<TCommandMessage>(TCommandMessage commandMessage) where TCommandMessage : ICommand;
        event EventHandler<CommandExecutedArgs> CommandExecuted;    
    }
}
