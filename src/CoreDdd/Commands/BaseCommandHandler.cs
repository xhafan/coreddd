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
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public virtual async Task ExecuteAsync(TCommand command)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
        }
#endif
    }
}