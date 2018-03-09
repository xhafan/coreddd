using System;

namespace CoreDdd.Commands
{
    public interface ICommandHandler<in TCommandMessage> where TCommandMessage: ICommand
    {
        void Execute(TCommandMessage command);
        event EventHandler<CommandExecutedArgs> CommandExecuted;
    }
}
