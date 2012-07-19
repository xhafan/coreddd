using System.Web.Mvc;
using Core.Queries;
using EmailMaker.Controllers;
using NUnit.Framework;

namespace EmailMaker.UnitTests.Controllers.Templates
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
