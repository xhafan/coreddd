using System.Linq;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.TestHelper.Builders.EmailTemplates;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.Domain.Tests.EmailTemplateTests
{
    [TestFixture]
    public class when_creating_variable
    {
        private EmailTemplate _emailTemplate;

        [SetUp]
        public void Context()
        {
            _emailTemplate = EmailTemplateBuilder.New
                .WithInitialHtml("this is an initial html")
                .Build();
            var htmlTeplatePartId = _emailTemplate.Parts.First().Id;
            _emailTemplate.CreateVariable(htmlTeplatePartId, 8, 2);
            _emailTemplate.CreateVariable(htmlTeplatePartId, 5, 2);
        }

        [Test]
        public void variable_created_correctly()
        {
            _emailTemplate.Parts.Count().ShouldBe(5);
            var htmlOne = (HtmlEmailTemplatePart)_emailTemplate.Parts.First();
            var variableOne = (VariableEmailTemplatePart)_emailTemplate.Parts.ElementAt(1);
            var htmlTwo = (HtmlEmailTemplatePart)_emailTemplate.Parts.ElementAt(2);
            var variableTwo = (VariableEmailTemplatePart)_emailTemplate.Parts.ElementAt(3);
            var htmlThree = (HtmlEmailTemplatePart)_emailTemplate.Parts.Last();
            htmlOne.Html.ShouldBe("this ");
            variableOne.Value.ShouldBe("is");
            htmlTwo.Html.ShouldBe(" ");
            variableTwo.Value.ShouldBe("an");
            htmlThree.Html.ShouldBe(" initial html");
        }
    }
}