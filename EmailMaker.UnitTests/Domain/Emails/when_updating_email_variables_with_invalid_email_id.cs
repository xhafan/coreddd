using Core.Utilities;
using EmailMaker.Dtos.Emails;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.Emails
{
    [TestFixture]
    public class when_updating_email_variables_with_invalid_email_id
    {
        private CoreException _exception;

        [SetUp]
        public void Context()
        {
            var template = EmailTemplateBuilder.New
                .WithInitialHtml("12345")
                .Build();
            const int emailId = 78;
            var email = EmailBuilder.New
                .WithId(56)
                .WithEmailTemplate(template)
                .Build();
            var emailDto = new EmailDto {EmailId = emailId};
            _exception = Should.Throw<CoreException>(() => email.UpdateVariables(emailDto));
        }

        [Test]
        public void exception_was_thrown()
        {
            _exception.Message.ToLower().ShouldMatch("invalid email id");
        }

    }
}