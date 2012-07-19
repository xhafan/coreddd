using System.Linq;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Dtos;
using EmailMaker.Dtos.EmailTemplates;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.EmailTemplates
{
    [TestFixture]
    public class when_updating_email_template
    {
        private EmailTemplate _template;
        private int _templateId;
        private string _templateName = "template name";

        [SetUp]
        public void Context()
        {
            _templateId = 89;
            _template = EmailTemplateBuilder.New
                .WithId(_templateId)
                .WithInitialHtml("12345")
                .WithVariable(1, 1)
                .WithVariable(1, 1)
                .Build();
            var emailTemplateDTO = new EmailTemplateDto
                                       {
                                           EmailTemplateId = _templateId,
                                           Parts = new[]
                                                       {
                                                           new EmailTemplatePartDto
                                                               {
                                                                   PartId = _template.Parts.First().Id,
                                                                   PartType = PartType.Html,
                                                                   Html = "A"
                                                               },
                                                           new EmailTemplatePartDto
                                                               {
                                                                   PartId = _template.Parts.ElementAt(3).Id,
                                                                   PartType = PartType.Variable,
                                                                   VariableValue = "B"
                                                               },
                                                           new EmailTemplatePartDto
                                                               {
                                                                   PartId = _template.Parts.ElementAt(4).Id,
                                                                   PartType = PartType.Html,
                                                                   Html = "C"
                                                               },
                                                       },
                                                       Name = _templateName
                                       };
            _template.Update(emailTemplateDTO);
        }

        [Test]
        public void template_was_updated()
        {
            (_template.Parts.ElementAt(0) as HtmlEmailTemplatePart).Html.ShouldBe("A");
            (_template.Parts.ElementAt(1) as VariableEmailTemplatePart).Value.ShouldBe("2");
            (_template.Parts.ElementAt(2) as HtmlEmailTemplatePart).Html.ShouldBe("3");
            (_template.Parts.ElementAt(3) as VariableEmailTemplatePart).Value.ShouldBe("B");
            (_template.Parts.ElementAt(4) as HtmlEmailTemplatePart).Html.ShouldBe("C");
            _template.Name.ShouldBe(_templateName);
        }
    }
}