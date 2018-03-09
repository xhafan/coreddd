using System.Collections.Generic;
using EmailMaker.Core;
using EmailMaker.Domain.Emails;
using EmailMaker.Service.Handlers;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.Service.Tests.Handlers
{
    [TestFixture]
    public class when_building_email_html_with_unknown_email_part
    {
        private class UnknownPart : EmailPart { }

        private EmailMakerException _exception;

        [SetUp]
        public void Context()
        {
            var parts = new List<EmailPart>
                            {
                                new UnknownPart()
                            };

            var emailHtmlBuilder = new EmailHtmlBuilder();
            _exception = Should.Throw<EmailMakerException>(() => emailHtmlBuilder.BuildHtmlEmail(parts));
        }

        [Test]
        public void email_html_is_correct()
        {
            _exception.Message.ShouldBe("Unknown part type: " + typeof(UnknownPart));
        }
    }
}