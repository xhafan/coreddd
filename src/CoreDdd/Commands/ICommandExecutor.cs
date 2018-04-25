using System;
#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Commands
{
    public interface ICommandExecutor
    {
        void Execute<TCommand>(TCommand command) where TCommand : ICommand;
        event Action<CommandExecutedArgs> CommandExecuted;

#if !NET40
        Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;
#endif
    }
}
