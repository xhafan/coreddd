using System;
#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Commands
{
    public abstract class BaseCommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public virtual void Execute(TCommand command)
        {
        }

        public event Action<CommandExecutedArgs> CommandExecuted;

        protected void RaiseCommandExecutedEvent(CommandExecutedArgs eventArgs)
        {
            CommandExecuted?.Invoke(eventArgs);
        }

#if !NET40
        public async virtual Task ExecuteAsync(TCommand command)
        {
        }
#endif
    }
}