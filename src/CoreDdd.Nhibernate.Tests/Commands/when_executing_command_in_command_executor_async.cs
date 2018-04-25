#if !NET40
using System.Threading.Tasks;
using CoreDdd.Commands;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Commands
{
    [TestFixture]
    public class when_executing_command_in_command_executor_async
    {
        private bool _commandExecutedEventWasRaised;

        [SetUp]
        public async Task Context()
        {
            var testCommand = new TestCommand {CommandExecutedArgs = "args"};

            var commandExecutor = new CommandExecutor();
            commandExecutor.CommandExecuted += args =>
            {
                if ((string) args.Args == "args") _commandExecutedEventWasRaised = true;
            };


            await commandExecutor.ExecuteAsync(testCommand);
        }

        [Test]
        public void command_executed_by_command_handler()
        {
            _commandExecutedEventWasRaised.ShouldBe(true);
        }    
    }
}
#endif