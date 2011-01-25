using System.Linq;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.TestHelper.Builders.EmailTemplates;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.Domain.Tests.EmailTests
{
    [TestFixture]
    public class when_creating_email
    {
        private Email _email;
        private EmailTemplate _template;

        [SetUp]
        public void Context()
        {
            _template = EmailTemplateBuilder.New
                .WithInitialHtml("123")
                .WithVariable(1, 1)
                .Build();
            _email = new Email(_template);
        }

        [Test]
        public void Test()
        {
            _email.Parts.Count().ShouldBe(_template.Parts.Count());

            var htmlPart = (HtmlEmailPart)_email.Parts.First();
            var htmlEmailTemplatePart = (HtmlEmailTemplatePart)_template.Parts.First();
            htmlPart.Html.ShouldBe(htmlEmailTemplatePart.Html);
            
            var variablePart = (VariableEmailPart)_email.Parts.ElementAt(1);
            var variableTemplatePart = (VariableEmailTemplatePart) _template.Parts.ElementAt(1);
            variablePart.VariableType.ShouldBe(variableTemplatePart.VariableType);
            variablePart.Value.ShouldBe(variableTemplatePart.Value);

            htmlPart = (HtmlEmailPart)_email.Parts.Last();
            htmlEmailTemplatePart = (HtmlEmailTemplatePart)_template.Parts.Last();
            htmlPart.Html.ShouldBe(htmlEmailTemplatePart.Html);
        }
    }
}
