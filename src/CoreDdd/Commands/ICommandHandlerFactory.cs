namespace CoreDdd.Commands
{
    /// <summary>
    /// A factory to create a command handler for a given command type.
    /// Usually implemented auto-magically by an IoC container.
    /// </summary>
    public interface ICommandHandlerFactory
    {
        /// <summary>
        /// Create an instance of a command handler for a given command type.
        /// </summary>
        /// <typeparam name="TCommand">A command type</typeparam>
        /// <returns>Instance of a command handler</returns>
        ICommandHandler<TCommand> Create<TCommand>() where TCommand : ICommand;

        /// <summary>
        /// Releases a command handler instance previously created by <see cref="Create{TCommand}"/> method.
        /// This is needed by Castle Windsor IoC container, other IoC containers (e.g. Ninject, ServiceProvider) don't seem to
        /// support it.
        /// </summary>
        /// <typeparam name="TCommand">A command type</typeparam>
        /// <param name="commandHandler">A command handler instance</param>
        void Release<TCommand>(ICommandHandler<TCommand> commandHandler) where TCommand : ICommand;
    }
}