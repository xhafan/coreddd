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
        private int _userId = 123;

        [SetUp]
        public void Context()
        {
            _emailTemplate = new EmailTemplate(_userId);
        }

        [Test]
        public void email_template_created_correctly()
        {
            _emailTemplate.Parts.Count().ShouldBe(1);
            var htmlTemplatePart = (HtmlEmailTemplatePart) _emailTemplate.Parts.First();
            htmlTemplatePart.Position.ShouldBe(0);
            htmlTemplatePart.Html.ShouldBe(string.Empty);
            _emailTemplate.UserId.ShouldBe(_userId);
        }
    }
}
