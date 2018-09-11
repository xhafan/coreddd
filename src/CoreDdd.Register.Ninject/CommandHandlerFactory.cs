using CoreDdd.Commands;
using Ninject;
using Ninject.Syntax;

namespace CoreDdd.Register.Ninject
{
    public class CommandHandlerFactory : ICommandHandlerFactory
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
            // do nothing - Ninject does not have a concept of releasing components from typed factories like Castle Windsor
        }
    }
}