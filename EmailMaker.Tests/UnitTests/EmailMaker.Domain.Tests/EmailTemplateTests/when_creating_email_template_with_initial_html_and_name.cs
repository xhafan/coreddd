using EmailMaker.Domain.EmailTemplates;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.Domain.Tests.EmailTemplateTests
{
    [TestFixture]
    public class when_creating_email_template_with_name
    {
        private EmailTemplate _emailTemplate;
        private string _name = "name";

        [SetUp]
        public void Context()
        {
            _emailTemplate = new EmailTemplate("html", _name, 0);
        }

        [Test]
        public void email_template_created_correctly()
        {            
            _emailTemplate.Name.ShouldBe(_name);
        }
    }
}