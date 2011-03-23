using System.Linq;
using Core.TestHelper.Extensions;
using EmailMaker.Domain.Emails;
using EmailMaker.DTO;
using EmailMaker.DTO.Emails;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.Domain.Tests.EmailTests
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
            var emailId = 78;
            _email = EmailBuilder.New
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
                                                                   PartId = _email.Parts.ElementAt(1).Id,
                                                                   PartType = PartType.Variable,
                                                                   VariableValue = "A"
                                                               },
                                                           new EmailPartDTO
                                                               {
                                                                   PartId = _email.Parts.ElementAt(3).Id,
                                                                   PartType = PartType.Variable,
                                                                   VariableValue = "B"
                                                               },
                                                       }
                                       };
            _email.UpdateVariables(emailDTO);
        }

        [Test]
        public void email_variables_were_updated()
        {
            (_email.Parts.ElementAt(1) as VariableEmailPart).Value.ShouldBe("A");
            (_email.Parts.ElementAt(3) as VariableEmailPart).Value.ShouldBe("B");
        }
    }
}