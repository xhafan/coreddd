#if !NET40
using System.Threading.Tasks;
# endif
using System;
using CoreDdd.Commands;

namespace CoreDdd.Nhibernate.Tests.Commands
{
    public class TestCommandHandler : BaseCommandHandler<TestCommand>, IDisposable
    {
        public static bool IsDisposed { get; private set; }

        public TestCommandHandler()
        {
            IsDisposed = false;
        }

        public override void Execute(TestCommand command)
        {
            RaiseCommandExecutedEvent(new CommandExecutedArgs { Args = command.CommandExecutedArgs });
        }

#if !NET40
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Task ExecuteAsync(TestCommand command)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            RaiseCommandExecutedEvent(new CommandExecutedArgs { Args = command.CommandExecutedArgs });
        }
#endif
        public void Dispose()
        {
            IsDisposed = true;
        }
    }
}