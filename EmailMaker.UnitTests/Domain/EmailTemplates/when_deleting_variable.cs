using System.Linq;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.EmailTemplates
{
    [TestFixture]
    public class when_deleting_variable
    {
        private EmailTemplate _template;

        [SetUp]
        public void Context()
        {
            _template = EmailTemplateBuilder.New
                .WithInitialHtml("12345")
                .WithVariable(1, 1)
                .WithVariable(1, 1)
                .Build();
            var variablePartId = _template.Parts.ElementAt(1).Id;
            _template.DeleteVariable(variablePartId);
        }

        [Test]
        public void variable_is_deleted()
        {
            _template.Parts.Count().ShouldBe(3);
            
            var beforeHtmlPart = (HtmlEmailTemplatePart)_template.Parts.First();
            beforeHtmlPart.Html.ShouldBe("123");
            
            var variablePart = (VariableEmailTemplatePart)_template.Parts.ElementAt(1);
            variablePart.Value.ShouldBe("4");
            
            var afterHtmlPart = (HtmlEmailTemplatePart)_template.Parts.Last();
            afterHtmlPart.Html.ShouldBe("5");
        }
    }
}
