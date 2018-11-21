using System;
#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Commands
{
    /// <summary>
    /// Represents a command handler. You can implement this interface on your command handler directly,
    /// or you can derive you command handler from <see cref="BaseCommandHandler{TCommand}"/>
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface ICommandHandler<in TCommand> where TCommand: ICommand
    {
        /// <summary>
        /// Implements the command handling logic.
        /// </summary>
        /// <param name="command">A command instance with a data</param>
        void Execute(TCommand command);

        /// <summary>
        /// Command executed event. Can be used for instance to raise an event with generated id for an aggregate root domain entity. 
        /// Commands should not return any values - to return values, use queries instead of commands.
        /// </summary>
        event Action<CommandExecutedArgs> CommandExecuted;

#if !NET40
        /// <summary>
        /// Implement the command handling logic asynchronously.
        /// </summary>
        /// <param name="command">A command instance with a data</param>
        Task ExecuteAsync(TCommand command);
#endif
    }
}
