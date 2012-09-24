using System.Linq;
using CoreDdd.Utilities;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.EmailTemplates
{
    [TestFixture]
    public class when_deleting_variable_with_html_part_id
    {
        private EmailTemplate _template;
        private CoreException _exception;

        [SetUp]
        public void Context()
        {
            _template = EmailTemplateBuilder.New
                .WithInitialHtml("123")
                .WithVariable(1, 1)
                .Build();
            var htmlPartId = _template.Parts.Last().Id;
            _exception = Should.Throw<CoreException>(() => _template.DeleteVariable(htmlPartId));
        }

        [Test]
        public void exception_was_thrown()
        {
            _exception.Message.ToLower().ShouldMatch("part is not a variable, id: 2");
        }
    }
}