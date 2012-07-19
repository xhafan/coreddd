using System.Linq;
using Core.Utilities;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;

namespace EmailMaker.UnitTests.Domain.EmailTemplates
{
    [TestFixture]
    public class when_deleting_variable_with_html_part_id
    {
        private EmailTemplate _template;

        [Test]
        [ExpectedException(typeof(CoreException), ExpectedMessage = "Part is not a variable, Id: 3")]
        public void Context()
        {
            _template = EmailTemplateBuilder.New
                .WithInitialHtml("123")
                .WithVariable(1, 1)
                .Build();
            var htmlPartId = _template.Parts.Last().Id;
            _template.DeleteVariable(htmlPartId);
        }
    }
}