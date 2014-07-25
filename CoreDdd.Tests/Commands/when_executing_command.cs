using System;
using CoreDdd.Commands;
using CoreIoC;
using CoreTest;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace CoreDdd.Tests.Commands
{
    [TestFixture]
    public class when_executing_command : BaseTest
    {        
        public class TestCommand : ICommand { }
        public class TestCommandHandler : ICommandHandler<TestCommand>
        {
            public bool CommandWasExecuted;
            public bool EventHandlerWasAssigned
            {
                get
                {
                    return CommandExecuted != null;
                }
            }
            
            public void Execute(TestCommand command)
            {
                CommandWasExecuted = true;
            }

            public event EventHandler<CommandExecutedArgs> CommandExecuted;
        }

        private TestCommandHandler _testCommandHandler;

        [SetUp]
        public void Context()
        {
            var container = Stub<IContainer>();
            IoC.Initialize(container);

            _testCommandHandler = new TestCommandHandler();
            var testCommand = new TestCommand();

            container.Stub(a => a.Resolve<ICommandHandler<TestCommand>>()).Return(_testCommandHandler);

            var commandExecutor = new CommandExecutor();
            commandExecutor.CommandExecuted += (sender, args) => {};
            commandExecutor.Execute(testCommand);
        }

        [Test]
        public void command_was_executed_by_handler()
        {
            _testCommandHandler.CommandWasExecuted.ShouldBe(true);
        }

        [Test]
        public void event_handlers_was_correctly_set()
        {
            _testCommandHandler.EventHandlerWasAssigned.ShouldBe(true);
        }
    
    }
}