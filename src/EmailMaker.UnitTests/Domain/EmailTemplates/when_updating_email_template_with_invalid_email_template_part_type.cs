using EmailMaker.Core;
using EmailMaker.Dtos.EmailTemplates;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.EmailTemplates
{
    [TestFixture]
    public class when_updating_email_template_with_invalid_email_template_part_type
    {
        private EmailMakerException _exception;

        [SetUp]
        public void Context()
        {
            var template = EmailTemplateBuilder.New
                .WithId(45)
                .WithInitialHtml("12345")
                .Build();
            var emailTemplateDto = new EmailTemplateDto
                                       {
                                           EmailTemplateId = 45,
                                           Parts = new[] { new EmailTemplatePartDto() }
                                       };
            _exception = Should.Throw<EmailMakerException>(() => template.Update(emailTemplateDto));
        }

        [Test]
        public void exception_was_thrown()
        {
            _exception.Message.ToLower().ShouldMatch("unknown email template part type: none");
        }
    }
}