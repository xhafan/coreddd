using CoreTest;
using EmailMaker.Domain.Emails;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.Emails
{
    [TestFixture]
    public class when_creating_html_email_part : BaseTest
    {
        private const string Html = "html";
        private HtmlEmailPart _htmlEmailPart;

        [SetUp]
        public void Context()
        {
            _htmlEmailPart = new HtmlEmailPart(Html);
        }

        [Test]
        public void variable_email_part_correctly_created()
        {
            _htmlEmailPart.Html.ShouldBe(Html);
        }
    }
}