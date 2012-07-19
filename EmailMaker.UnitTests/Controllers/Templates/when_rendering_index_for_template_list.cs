using System.Web.Mvc;
using EmailMaker.Controllers;
using NUnit.Framework;

namespace EmailMaker.UnitTests.Controllers.Templates
{
    [TestFixture]
    public class when_rendering_index_for_template_list : BaseEmailMakerControllerTest
    {
        private ActionResult _result;

        public override void Context()
        {
            var controller = new TemplateController(CommandExecutor, QueryExecutor);
            // todo fix this test
            //_result = controller.Index();
        }

        [Test]
        public void result_is_correct()
        {
            //(_result is ViewResult).ShouldBe(true);
        }
    }
}