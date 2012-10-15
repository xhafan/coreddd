using System.Linq;
using EmailMaker.Domain.EmailTemplates;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.EmailTemplates
{
    [TestFixture]
    public class when_creating_email_template
    {
        private EmailTemplate _emailTemplate;
        private const int UserId = 123;

        [SetUp]
        public void Context()
        {
            _emailTemplate = new EmailTemplate(UserId);
        }

        [Test]
        public void email_template_created_correctly()
        {
            _emailTemplate.Parts.Count().ShouldBe(1);
            var htmlTemplatePart = (HtmlEmailTemplatePart) _emailTemplate.Parts.First();
            htmlTemplatePart.Html.ShouldBe(string.Empty);
            _emailTemplate.UserId.ShouldBe(UserId);
        }
    }
}
