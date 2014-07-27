using EmailMaker.Commands.Messages;
using EmailMaker.Controllers;
using NUnit.Framework;
using Rhino.Mocks;

namespace EmailMaker.UnitTests.Controllers.Templates
{
    [TestFixture]
    public class when_creating_a_variable : BaseEmailmakerControllerTest
    {
        private CreateVariableCommand _createVariableCommand;

        [SetUp]
        public override void Context()
        {
            base.Context();

            var controller = new TemplateController(CommandExecutor, null);
            _createVariableCommand = new CreateVariableCommand();
            controller.CreateVariable(_createVariableCommand);
        }

        [Test]
        public void command_was_executed()
        {
            CommandExecutor.AssertWasCalled(a => a.Execute(_createVariableCommand));
        }
    }
}