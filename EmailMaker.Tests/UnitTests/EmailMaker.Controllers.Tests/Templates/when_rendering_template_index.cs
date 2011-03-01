using System.Web.Mvc;
using Core.TestHelper.Controllers;
using EmailMaker.Controllers.Template;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.Controllers.Tests.Templates
{
    [TestFixture]
    public class when_rendering_template_index : BaseControllerTest
    {
        private ActionResult _result;

        public override void Context()
        {
            var controller = new TemplateController(null);
            _result = controller.Index();
        }

        [Test]
        public void result_is_correct()
        {
            (_result is ViewResult).ShouldBe(true);
        }
    }
}
