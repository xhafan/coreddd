using System.Web.Mvc;
using Core.TestHelper.Controllers;
using EmailMaker.Controllers.Template;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.Controllers.Tests.Templates
{
    [TestFixture]
    public class when_rendering_new_template : BaseControllerTest
    {
        private ActionResult _result;

        public override void Context()
        {                        
            var controller = new TemplateController(CommandExecutor);
            _result = controller.New();
        }

        [Test]
        public void result_is_correct()
        {
            (_result is ViewResult).ShouldBe(true);
        }
    }
}