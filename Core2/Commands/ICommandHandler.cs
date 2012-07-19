using System;

namespace Core.Commands
{
    public interface ICommandHandler<in TCommandMessage> where TCommandMessage: ICommand
    {
        void Execute(TCommandMessage command);
        event EventHandler<CommandExecutedArgs> CommandExecuted;
    }
}
