using CoreDdd.Commands;
using Ninject;
using Ninject.Syntax;

namespace CoreDdd.Register.Ninject
{
    internal class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IResolutionRoot _ninjectIoCContainer;

        public CommandHandlerFactory(IResolutionRoot ninjectIoCContainer)
        {
            _ninjectIoCContainer = ninjectIoCContainer;
        }

        public ICommandHandler<TCommand> Create<TCommand>() where TCommand : ICommand
        {
            return _ninjectIoCContainer.Get<ICommandHandler<TCommand>>();
        }

        public void Release<TCommand>(ICommandHandler<TCommand> commandHandler) where TCommand : ICommand
        {
            _ninjectIoCContainer.Release(commandHandler);
        }
    }
}