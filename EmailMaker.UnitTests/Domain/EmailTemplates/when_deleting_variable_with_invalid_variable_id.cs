using CoreUtils;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.EmailTemplates
{
    [TestFixture]
    public class when_deleting_variable_with_invalid_variable_id
    {
        private EmailTemplate _template;
        private CoreException _exception;

        [SetUp]
        public void Context()
        {
            _template = EmailTemplateBuilder.New.Build();
            _exception = Should.Throw<CoreException>(() => _template.DeleteVariable(23));
        }

        [Test]
        public void exception_was_thrown()
        {
            _exception.Message.ToLower().ShouldMatch("invalid variable part id: 23");
        }
    }
}