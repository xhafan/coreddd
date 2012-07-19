using System;

namespace Core.Commands
{
    public interface ICommandExecutor
    {
        void Execute<TCommandMessage>(TCommandMessage commandMessage) where TCommandMessage : ICommand;
        event EventHandler<CommandExecutedArgs> CommandExecuted;    
    }
}
