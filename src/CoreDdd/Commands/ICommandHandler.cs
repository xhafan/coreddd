using System;
#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand: ICommand
    {
        void Execute(TCommand command);
        event Action<CommandExecutedArgs> CommandExecuted;

#if !NET40
        Task ExecuteAsync(TCommand command);
#endif
    }
}
