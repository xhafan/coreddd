using EmailMaker.DTO.EmailTemplates;
using EmailMaker.TestHelper.Builders.EmailTemplates;
using EmailMaker.Utilities;
using NUnit.Framework;

namespace EmailMaker.Domain.Tests.EmailTemplateTests
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
            var emailTemplateDTO = new EmailTemplateDTO
                                       {
                                           EmailTemplateId = 45,
                                           Parts = new[] { new EmailTemplatePartDTO() }
                                       };
            template.Update(emailTemplateDTO);
        }
    }
}