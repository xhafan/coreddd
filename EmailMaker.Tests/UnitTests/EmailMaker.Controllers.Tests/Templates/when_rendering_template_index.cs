using System.Web.Mvc;
using Core.TestHelper.Controllers;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.Controllers.Tests.Templates
{
    [TestFixture]
    public class when_rendering_template_index : BaseEmailMakerControllerTest
    {
        private ActionResult _result;

        public override void Context()
        {
            var controller = new TemplateController(null, QueryExecutor);
            // todo: fix this test
            //_result = controller.Index();
        }

        [Test]
        public void result_is_correct()
        {
            //(_result is ViewResult).ShouldBe(true);
        }
    }
}
