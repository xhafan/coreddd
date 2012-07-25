using System.Linq;
using EmailMaker.Domain.EmailTemplates;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.EmailTemplates
{
    [TestFixture]
    public class when_creating_email_template_with_initial_html
    {
        private EmailTemplate _emailTemplate;
        private const string Html = "html";

        [SetUp]
        public void Context()
        {
            _emailTemplate = new EmailTemplate(Html);
        }

        [Test]
        public void email_template_created_correctly()
        {
            _emailTemplate.Parts.Count().ShouldBe(1);
            var htmlTemplatePart = (HtmlEmailTemplatePart)_emailTemplate.Parts.First();
            htmlTemplatePart.Position.ShouldBe(0);
            htmlTemplatePart.Html.ShouldBe(Html);
        }
    }
}