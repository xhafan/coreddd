using System;
using CoreDdd.Commands;

namespace CoreDdd.Nhibernate.Tests.Commands
{
    public class TestCommandHandler : ICommandHandler<TestCommand>
    {
        public void Execute(TestCommand command)
        {
            CommandExecuted?.Invoke(this, new CommandExecutedArgs { Args = command.CommandExecutedArgs });
        }

        public event EventHandler<CommandExecutedArgs> CommandExecuted;
    }
}