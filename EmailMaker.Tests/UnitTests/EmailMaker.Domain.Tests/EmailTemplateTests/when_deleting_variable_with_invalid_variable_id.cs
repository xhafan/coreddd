using Core.Utilities;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;

namespace EmailMaker.Domain.Tests.EmailTemplateTests
{
    [TestFixture]
    public class when_deleting_variable_with_invalid_variable_id
    {
        private EmailTemplate _template;

        [Test]
        [ExpectedException(typeof(CoreException), ExpectedMessage = "Invalid variable part Id: 23")]
        public void Context()
        {
            _template = EmailTemplateBuilder.New.Build();
            _template.DeleteVariable(23);
        }
    }
}