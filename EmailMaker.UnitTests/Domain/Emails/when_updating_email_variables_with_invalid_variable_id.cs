using System;
using EmailMaker.Dtos;
using EmailMaker.Dtos.Emails;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;

namespace EmailMaker.UnitTests.Domain.Emails
{
    [TestFixture]
    public class when_updating_email_variables_with_invalid_variable_id
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Sequence contains no matching element")]
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
            var emailDTO = new EmailDto
                               {
                                   EmailId = emailId,
                                   Parts = new[]
                                               {
                                                   new EmailPartDto
                                                       {
                                                           PartId = 123,
                                                           PartType = PartType.Variable,
                                                           VariableValue = "A"
                                                       },
                                               }
                               };
            email.UpdateVariables(emailDTO);
        }
    }
}