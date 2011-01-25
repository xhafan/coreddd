using System.Linq;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.TestHelper.Builders.EmailTemplates;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.Domain.Tests.EmailTemplateTests
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
            
            var beforeHtmlPart = _template.Parts.First() as HtmlEmailTemplatePart;
            beforeHtmlPart.Html.ShouldBe("123");
            
            var variablePart = _template.Parts.ElementAt(1) as VariableEmailTemplatePart;
            variablePart.Value.ShouldBe("4");
            
            var afterHtmlPart = _template.Parts.Last() as HtmlEmailTemplatePart;
            afterHtmlPart.Html.ShouldBe("5");
        }
    }
}
