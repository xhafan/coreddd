using System;
using Core.Commons;

namespace Core.Commands
{
    public class CommandExecutor : ICommandExecutor
    {
        public void Execute<TCommandMessage>(TCommandMessage commandMessage) where TCommandMessage : ICommandMessage
        {
            var handler = IoC.Resolve<ICommandMessageHandler<TCommandMessage>>();
            handler.Execute(commandMessage);
        }
    }
}