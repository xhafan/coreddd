using CoreDdd.Commands;

namespace CoreDdd.Nhibernate.Tests.Commands
{
    public class TestCommandHandler : BaseCommandHandler<TestCommand>
    {
        public override void Execute(TestCommand command)
        {
            RaiseCommandExecutedEvent(new CommandExecutedArgs { Args = command.CommandExecutedArgs });
        }
    }
}