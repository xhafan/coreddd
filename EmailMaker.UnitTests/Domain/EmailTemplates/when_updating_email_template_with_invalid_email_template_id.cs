using Core.Utilities;
using EmailMaker.Dtos.EmailTemplates;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;

namespace EmailMaker.UnitTests.Domain.EmailTemplates
{
    [TestFixture]
    public class when_updating_email_template_with_invalid_email_template_id
    {
        [Test]
        [ExpectedException(typeof(CoreException), ExpectedMessage = "Invalid email template id")]
        public void Context()
        {
            var template = EmailTemplateBuilder.New
                .WithId(45)
                .WithInitialHtml("12345")
                .Build();
            var emailTemplateDTO = new EmailTemplateDto
                                       {
                                           EmailTemplateId = 66,
                                       };
            template.Update(emailTemplateDTO);
        }
    }
}