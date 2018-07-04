namespace CoreDdd.Commands
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<TCommand> Create<TCommand>() where TCommand : ICommand;
        void Release<TCommand>(ICommandHandler<TCommand> commandHandler) where TCommand : ICommand;
    }
}