using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Domain.EmailTemplates.VariableTypes;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.VariableEmailTemplateParts
{
    [TestFixture]
    public class when_creating_variable_email_template_part
    {
        private VariableEmailTemplatePart _variableEmailTemplatePart;
        private const string Value = "value";

        [SetUp]
        public void Context()
        {
            _variableEmailTemplatePart = new VariableEmailTemplatePart(Value);
        }

        [Test]
        public void properties_are_correctly_set()
        {
            _variableEmailTemplatePart.Value.ShouldBe(Value);
            _variableEmailTemplatePart.VariableType.ShouldBe(VariableType.InputText);
        }
    }
}
