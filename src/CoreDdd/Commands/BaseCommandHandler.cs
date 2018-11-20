using System;
#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Commands
{
    /// <summary>
    /// The base class to derive your command handlers from. Override method Execute to implement the command handling logic,
    /// or override ExecuteAsync to implement the command handling logic asynchronously.
    /// </summary>
    /// <typeparam name="TCommand">The command type which implements <see cref="ICommand"/></typeparam>
    public abstract class BaseCommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// Override this method to implement the command handling logic when handling a command in a non-async way.
        /// </summary>
        /// <param name="command">The command instance with data</param>
        public virtual void Execute(TCommand command)
        {
        }

        /// <summary>
        /// Command executed event. Can be used for instance to raise an event with generated id for an aggregate root domain entity. 
        /// Commands should not return any values - to return values, use queries instead of commands.
        /// </summary>
        public event Action<CommandExecutedArgs> CommandExecuted;

        /// <summary>
        /// Raises a command executed event.
        /// </summary>
        /// <param name="eventArgs">Holds data generated during command execution</param>
        protected void RaiseCommandExecutedEvent(CommandExecutedArgs eventArgs)
        {
            CommandExecuted?.Invoke(eventArgs);
        }

#if !NET40
        /// <summary>
        /// Override this method to implement the command handling logic when handling a command in an async way.
        /// </summary>
        /// <param name="command">The command instance with data</param>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public virtual async Task ExecuteAsync(TCommand command)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
        }
#endif
    }
}