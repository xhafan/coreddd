using System.Collections.Generic;
using EmailMaker.Domain.Emails;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.Domain.Services.Tests
{
    [TestFixture]
    public class when_building_email_html
    {
        private string _result;
        private string _html1 = "html1";
        private string _value = "value";
        private string _html2 = "html2";

        [SetUp]
        public void Context()
        {
            var parts = new List<EmailPart>
                            {
                                new HtmlEmailPart(_html1),
                                new VariableEmailPart(VariableType.InputText, _value),
                                new HtmlEmailPart(_html2)
                            };

            var emailHtmlBuilder = new EmailHtmlBuilder();
            _result = emailHtmlBuilder.BuildHtmlEmail(parts);
        }

        [Test]
        public void email_html_is_correct()
        {
            _result.ShouldBe(_html1 + _value + _html2);
        }
    }

}
