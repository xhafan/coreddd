using Core.Utilities;
using EmailMaker.DTO.EmailTemplates;
using EmailMaker.TestHelper.Builders.EmailTemplates;
using NUnit.Framework;

namespace EmailMaker.Domain.Tests.EmailTemplateTests
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
            var emailTemplateDTO = new EmailTemplateDTO
                                       {
                                           EmailTemplateId = 66,
                                       };
            template.Update(emailTemplateDTO);
        }
    }
}