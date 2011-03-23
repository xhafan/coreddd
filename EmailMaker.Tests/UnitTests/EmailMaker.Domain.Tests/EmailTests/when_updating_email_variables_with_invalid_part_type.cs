using Core.TestHelper.Extensions;
using EmailMaker.Domain.Emails;
using EmailMaker.DTO;
using EmailMaker.DTO.Emails;
using EmailMaker.TestHelper.Builders;
using EmailMaker.Utilities;
using NUnit.Framework;

namespace EmailMaker.Domain.Tests.EmailTests
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
            var emailId = 78;
            var email = EmailBuilder.New
                .WithId(emailId)
                .WithEmailTemplate(template)
                .Build();
            var emailDTO = new EmailDTO
                               {
                                   EmailId = emailId,
                                   Parts = new[]
                                               {
                                                   new EmailPartDTO
                                                       {
                                                           PartType = PartType.Html
                                                       },
                                               }
                               };
            email.UpdateVariables(emailDTO);
        }
    }
}