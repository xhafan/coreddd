using System.Collections.Generic;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.EmailTemplates.VariableTypes;
using EmailMaker.Service.Handlers;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.Service.Tests.Handlers
{
    [TestFixture]
    public class when_building_email_html
    {
        private string _result;
        private const string HtmlOne = "html one";
        private const string Value = "value";
        private const string HtmlTwo = "html two";

        [SetUp]
        public void Context()
        {
            var parts = new List<EmailPart>
                            {
                                new HtmlEmailPart(HtmlOne),
                                new VariableEmailPart(VariableType.InputText, Value),
                                new HtmlEmailPart(HtmlTwo)
                            };

            var emailHtmlBuilder = new EmailHtmlBuilder();
            _result = emailHtmlBuilder.BuildHtmlEmail(parts);
        }

        [Test]
        public void email_html_is_correct()
        {
            _result.ShouldBe(HtmlOne + Value + HtmlTwo);
        }
    }

}
