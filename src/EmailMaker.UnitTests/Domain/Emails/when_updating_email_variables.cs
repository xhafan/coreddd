using System.Linq;
using EmailMaker.Domain.Emails;
using EmailMaker.Dtos;
using EmailMaker.Dtos.Emails;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.Emails
{
    [TestFixture]
    public class when_updating_email_variables
    {
        private Email _email;

        [SetUp]
        public void Context()
        {
            var template = EmailTemplateBuilder.New
                .WithInitialHtml("12345")
                .WithVariable(1, 1)
                .WithVariable(1, 1)
                .Build();
            const int emailId = 78;
            _email = new EmailBuilder()
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
                                                                   PartId = _email.Parts.ElementAt(1).Id,
                                                                   PartType = PartType.Variable,
                                                                   VariableValue = "A"
                                                               },
                                                           new EmailPartDto
                                                               {
                                                                   PartId = _email.Parts.ElementAt(3).Id,
                                                                   PartType = PartType.Variable,
                                                                   VariableValue = "B"
                                                               },
                                                       }
                                       };
            _email.UpdateVariables(emailDto);
        }

        [Test]
        public void email_variables_were_updated()
        {
            ((VariableEmailPart)_email.Parts.ElementAt(1)).Value.ShouldBe("A");
            ((VariableEmailPart)_email.Parts.ElementAt(3)).Value.ShouldBe("B");
        }
    }
}