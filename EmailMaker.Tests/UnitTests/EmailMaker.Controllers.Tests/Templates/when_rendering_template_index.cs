using System.Web.Mvc;
using EmailMaker.Controllers.Template;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.Controllers.Tests.Templates
{
    [TestFixture]
    public class when_rendering_template_index
    {
        private ActionResult _result;

        [SetUp]
        public void Context()
        {
            var controller = new TemplateController();
            _result = controller.Index();
        }

        [Test]
        public void result_is_correct()
        {
            (_result is ViewResult).ShouldBe(true);
        }
    }
}
