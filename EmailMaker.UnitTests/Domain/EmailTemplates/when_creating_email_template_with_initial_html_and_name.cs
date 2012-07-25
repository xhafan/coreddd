using EmailMaker.Domain.EmailTemplates;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.EmailTemplates
{
    [TestFixture]
    public class when_creating_email_template_with_name
    {
        private EmailTemplate _emailTemplate;
        private const string Name = "name";

        [SetUp]
        public void Context()
        {
            _emailTemplate = new EmailTemplate("html", Name, 0);
        }

        [Test]
        public void email_template_created_correctly()
        {            
            _emailTemplate.Name.ShouldBe(Name);
        }
    }
}