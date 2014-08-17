using System;
using EmailMaker.Dtos;
using EmailMaker.Dtos.Emails;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.Emails
{
    [TestFixture]
    public class when_updating_email_variables_with_invalid_variable_id
    {
        private InvalidOperationException _exception;

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
            _exception = Should.Throw<InvalidOperationException>(() => email.UpdateVariables(emailDto));
        }

        [Test]
        public void exception_was_thrown()
        {
            _exception.Message.ToLower().ShouldMatch("sequence contains no matching element");
        }
    }
}