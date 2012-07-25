using EmailMaker.Core;
using EmailMaker.Dtos;
using EmailMaker.Dtos.Emails;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;

namespace EmailMaker.UnitTests.Domain.Emails
{
    [TestFixture]
    public class when_updating_email_variables_with_invalid_part_type
    {
        [Test]
        [ExpectedException(typeof(EmailMakerException), ExpectedMessage = "Unknown email part type: Html")]
        public void Context()
        {
            var template = EmailTemplateBuilder.New
                .WithInitialHtml("12345")
                .Build();
            const int emailId = 78;
            var email = EmailBuilder.New
                .WithId(emailId)
                .WithEmailTemplate(template)
                .Build();
            var emailDto = new EmailDto
                               {
                                   EmailId = emailId,
                                   Parts = new[]
                                               {
                                                   new EmailPartDto
                                                       {
                                                           PartType = PartType.Html
                                                       },
                                               }
                               };
            email.UpdateVariables(emailDto);
        }
    }
}