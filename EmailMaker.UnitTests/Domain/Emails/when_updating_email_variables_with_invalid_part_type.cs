using EmailMaker.Core;
using EmailMaker.Dtos;
using EmailMaker.Dtos.Emails;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.Emails
{
    [TestFixture]
    public class when_updating_email_variables_with_invalid_part_type
    {
        private EmailMakerException _exception;

        [SetUp]
        public void Context()
        {
            var template = EmailTemplateBuilder.New
                .WithInitialHtml("12345")
                .Build();
            const int emailId = 78;
            var email = new EmailBuilder()
                .WithId(emailId)
                .WithEmailTemplate(template)
                .Build();
            var emailDto = new EmailDto
                               {
                                   EmailId = emailId,
                                   Parts = new[] {new EmailPartDto {PartType = PartType.Html}}
                               };
            _exception = Should.Throw<EmailMakerException>(() => email.UpdateVariables(emailDto));
        }

        [Test]
        public void exception_was_thrown()
        {
            _exception.Message.ToLower().ShouldMatch("unknown email part type");
        }
    }
}