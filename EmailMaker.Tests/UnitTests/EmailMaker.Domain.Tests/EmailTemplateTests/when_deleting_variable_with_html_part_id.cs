using System.Linq;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.TestHelper.Builders.EmailTemplates;
using EmailMaker.Utilities;
using NUnit.Framework;

namespace EmailMaker.Domain.Tests.EmailTemplateTests
{
    [TestFixture]
    public class when_deleting_variable_with_html_part_id
    {
        private EmailTemplate _template;

        [Test]
        [ExpectedException(typeof(EmailMakerException), ExpectedMessage = "Part is not a variable, Id: 3")]
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