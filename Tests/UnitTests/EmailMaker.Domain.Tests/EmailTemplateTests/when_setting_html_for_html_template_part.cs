using System.Linq;
using EmailMaker.Domain.EmailTemplates;
using NUnit.Framework;
using Shouldly;
using TestHelper.Builders.EmailTemplates;

namespace EmailMaker.Domain.Tests.EmailTemplateTests
{
    [TestFixture]
    public class when_setting_html_for_html_template_part
    {
        private int _htmlTemplatePartId;
        private EmailTemplate _emailTemplate;
        private string _html;

        [SetUp]
        public void Context()
        {
            _emailTemplate = EmailTemplateBuilder.New.Build();
            _htmlTemplatePartId = _emailTemplate.Parts.First().Id;
            _html = "html";
            _emailTemplate.SetHtml(_htmlTemplatePartId, _html);
        }

        [Test]
        public void html_is_set_correctly()
        {
            var htmlTemplatePart = _emailTemplate.Parts.First() as HtmlTemplatePart;
            htmlTemplatePart.Html.ShouldBe(_html);
        }
    }
}