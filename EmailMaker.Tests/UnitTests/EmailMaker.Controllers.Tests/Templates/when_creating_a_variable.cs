using System.Web.Mvc;
using Core.TestHelper.Controllers;
using EmailMaker.Commands.Messages;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.Controllers.Tests.Templates
{
    [TestFixture]
    public class when_creating_a_variable : BaseControllerTest
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