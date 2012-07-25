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
        private const string TemplateName = "template name";

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
                                                       Name = TemplateName
                                       };
            _template.Update(emailTemplateDTO);
        }

        [Test]
        public void template_was_updated()
        {
            ((HtmlEmailTemplatePart)_template.Parts.ElementAt(0)).Html.ShouldBe("A");
            ((VariableEmailTemplatePart)_template.Parts.ElementAt(1)).Value.ShouldBe("2");
            ((HtmlEmailTemplatePart)_template.Parts.ElementAt(2)).Html.ShouldBe("3");
            ((VariableEmailTemplatePart)_template.Parts.ElementAt(3)).Value.ShouldBe("B");
            ((HtmlEmailTemplatePart)_template.Parts.ElementAt(4)).Html.ShouldBe("C");
            _template.Name.ShouldBe(TemplateName);
        }
    }
}