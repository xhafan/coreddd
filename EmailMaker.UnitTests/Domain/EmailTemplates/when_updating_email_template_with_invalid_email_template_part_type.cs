using EmailMaker.Core;
using EmailMaker.Dtos.EmailTemplates;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;

namespace EmailMaker.UnitTests.Domain.EmailTemplates
{
    [TestFixture]
    public class when_updating_email_template_with_invalid_email_template_part_type
    {
        [Test]
        [ExpectedException(typeof(EmailMakerException), ExpectedMessage = "Unknown email template part type: None")]
        public void Context()
        {
            var template = EmailTemplateBuilder.New
                .WithId(45)
                .WithInitialHtml("12345")
                .Build();
            var emailTemplateDTO = new EmailTemplateDto
                                       {
                                           EmailTemplateId = 45,
                                           Parts = new[] { new EmailTemplatePartDto() }
                                       };
            template.Update(emailTemplateDTO);
        }
    }
}