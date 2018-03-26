using System;
using CoreDdd.Commands;
using CoreIoC;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Tests.Commands
{
    [TestFixture]
    public class when_executing_command
    {
        private TestCommandHandler _testCommandHandler;
        private bool _commandExecutedEventWasRaised;

        [SetUp]
        public void Context()
        {
            _testCommandHandler = new TestCommandHandler();
            var testCommand = new TestCommand();

            _setupContainerToResolveCommandHandler();

            var commandExecutor = new CommandExecutor();
            commandExecutor.CommandExecuted += (sender, args) =>
            {
                if ((string) args.Args == "args") _commandExecutedEventWasRaised = true;
            };


            commandExecutor.Execute(testCommand);


            void _setupContainerToResolveCommandHandler()
            {
                var container = A.Fake<IContainer>();
                IoC.Initialize(container);
                A.CallTo(() => container.Resolve<ICommandHandler<TestCommand>>()).Returns(_testCommandHandler);
            }
        }

        [Test]
        public void command_was_executed_by_command_handler()
        {
            _testCommandHandler.CommandWasExecuted.ShouldBe(true);
        }

        [Test]
        public void command_executed_event_was_raised()
        {
            _commandExecutedEventWasRaised.ShouldBe(true);
        }

        public class TestCommand : ICommand
        {
        }

        public class TestCommandHandler : ICommandHandler<TestCommand>
        {
            public bool CommandWasExecuted;

            public void Execute(TestCommand command)
            {
                CommandWasExecuted = true;
                CommandExecuted?.Invoke(this, new CommandExecutedArgs { Args = "args" });
            }

            public event EventHandler<CommandExecutedArgs> CommandExecuted;
        }
    }
}