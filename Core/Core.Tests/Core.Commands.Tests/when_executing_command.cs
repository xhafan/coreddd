using Castle.Windsor;
using Core.Commons;
using NUnit.Framework;
using Rhino.Mocks;

namespace Core.Commands.Tests
{
    [TestFixture]
    public class when_executing_command
    {
        private ICommandMessageHandler<TestCommandMessage> _testCommandMessageHandler;
        private TestCommandMessage _testCommandMessage;

        public class TestCommandMessage : ICommandMessage { }

        [SetUp]
        public void Context()
        {
            var container = MockRepository.GenerateStub<IWindsorContainer>();
            IoC.Initialize(container);

            _testCommandMessageHandler = MockRepository.GenerateMock<ICommandMessageHandler<TestCommandMessage>>();
            _testCommandMessage = new TestCommandMessage();

            container.Stub(a => a.Resolve<ICommandMessageHandler<TestCommandMessage>>()).Return(_testCommandMessageHandler);

            var commandExecutor = new CommandExecutor();
            commandExecutor.Execute(_testCommandMessage);
        }

        [Test]
        public void command_was_executed_by_handler()
        {
            _testCommandMessageHandler.AssertWasCalled(a => a.Execute(_testCommandMessage));
        }
    }
}