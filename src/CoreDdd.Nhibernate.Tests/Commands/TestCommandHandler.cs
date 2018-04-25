#if !NET40
using System.Threading.Tasks;
# endif
using CoreDdd.Commands;

namespace CoreDdd.Nhibernate.Tests.Commands
{
    public class TestCommandHandler : BaseCommandHandler<TestCommand>
    {
        public override void Execute(TestCommand command)
        {
            RaiseCommandExecutedEvent(new CommandExecutedArgs { Args = command.CommandExecutedArgs });
        }

#if !NET40
        public async override Task ExecuteAsync(TestCommand command)
        {
            RaiseCommandExecutedEvent(new CommandExecutedArgs { Args = command.CommandExecutedArgs });
        }
#endif
    }
}