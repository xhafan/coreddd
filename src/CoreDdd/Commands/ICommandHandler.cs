using System;

namespace CoreDdd.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand: ICommand
    {
        void Execute(TCommand command);
        event Action<CommandExecutedArgs> CommandExecuted;
    }
}
