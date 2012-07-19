using EmailMaker.Commands.Messages;
using EmailMaker.Controllers;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Controllers.Templates
{
    [TestFixture]
    public class when_creating_a_variable : BaseEmailMakerControllerTest
    {
        private CreateVariableCommand _createVariableCommand;

        public override void Context()
        {
            var controller = new TemplateController(CommandExecutor, null);
            _createVariableCommand = new CreateVariableCommand();
            controller.CreateVariable(_createVariableCommand);
        }

        [Test]
        public void command_was_executed()
        {
            CommandExecutor.ShouldHaveBeenCalled(a => a.Execute(_createVariableCommand));
        }
    }
}