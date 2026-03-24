using System;
using CoreDdd.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace CoreDdd.Register.DependencyInjection
{
    internal class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        ICommandHandler<TCommand> ICommandHandlerFactory.Create<TCommand>()
        {
            return _serviceProvider.GetService<ICommandHandler<TCommand>>();
        }

        public void Release<TCommand>(ICommandHandler<TCommand> commandHandler) where TCommand : ICommand
        {
        }
    }
}