using System.Linq;
using EmailMaker.Domain.EmailTemplates;
using NUnit.Framework;
using Shouldly;
using TestHelper.Builders.EmailTemplates;

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
                .WithFakeIds()
                .WithInitialHtml("this initial html")
                .Build();
            var htmlTeplatePartId = _emailTemplate.Parts.First().Id;
            _emailTemplate.CreateVariable(htmlTeplatePartId, 5, 7);
        }

        [Test]
        public void variable_created_correctly()
        {
            _emailTemplate.Parts.Count().ShouldBe(3);
            var htmlBefore = (HtmlEmailTemplatePart)_emailTemplate.Parts.First();
            var variable = (VariableEmailTemplatePart)_emailTemplate.Parts.ElementAt(1);
            var htmlAfter = (HtmlEmailTemplatePart)_emailTemplate.Parts.Last();
            htmlBefore.Html.ShouldBe("this ");
            htmlBefore.Position.ShouldBe(0);
            variable.Value.ShouldBe("initial");
            variable.Position.ShouldBe(1);
            htmlAfter.Html.ShouldBe(" html");
            htmlAfter.Position.ShouldBe(2);
        }
    }
}