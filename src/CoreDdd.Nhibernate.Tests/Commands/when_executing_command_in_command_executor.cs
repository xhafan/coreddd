using CoreDdd.Commands;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Commands
{
    [TestFixture]
    public class when_executing_command_in_command_executor
    {
        private bool _commandExecutedEventWasRaised;

        [SetUp]
        public void Context()
        {
            var testCommand = new TestCommand {CommandExecutedArgs = "args"};

            var commandExecutor = new CommandExecutor();
            commandExecutor.CommandExecuted += (sender, args) =>
            {
                if ((string) args.Args == "args") _commandExecutedEventWasRaised = true;
            };


            commandExecutor.Execute(testCommand);
        }

        [Test]
        public void command_executed_by_command_handler()
        {
            _commandExecutedEventWasRaised.ShouldBe(true);
        }    
    }
}