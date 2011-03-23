using Core.Utilities;
using EmailMaker.DTO.Emails;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;

namespace EmailMaker.Domain.Tests.EmailTests
{
    [TestFixture]
    public class when_updating_email_variables_with_invalid_email_id
    {
        [Test]
        [ExpectedException(typeof(CoreException), ExpectedMessage = "Invalid email id")]
        public void Context()
        {
            var template = EmailTemplateBuilder.New
                .WithInitialHtml("12345")
                .Build();
            var emailId = 78;
            var email = EmailBuilder.New
                .WithId(56)
                .WithEmailTemplate(template)
                .Build();
            var emailDTO = new EmailDTO {EmailId = emailId};
            email.UpdateVariables(emailDTO);
        }
    }
}