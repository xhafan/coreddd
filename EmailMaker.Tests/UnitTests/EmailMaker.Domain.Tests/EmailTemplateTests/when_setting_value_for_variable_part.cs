using System.Linq;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.TestHelper.Builders.EmailTemplates;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.Domain.Tests.EmailTemplateTests
{
    [TestFixture]
    public class when_setting_value_for_variable_part
    {
        private int _variablePartId;
        private EmailTemplate _emailTemplate;
        private string _html;
        private string _value;

        [SetUp]
        public void Context()
        {
            _emailTemplate = EmailTemplateBuilder.New
                .WithInitialHtml("abc")
                .WithVariable(1,1)
                .Build();
            _variablePartId = _emailTemplate.Parts.ElementAt(1).Id;
            _value = "value";
            _emailTemplate.SetVariableValue(_variablePartId, _value);
        }

        [Test]
        public void variable_value_was_set_correctly()
        {
            var variableEmailTemplatePart = _emailTemplate.Parts.ElementAt(1) as VariableEmailTemplatePart;
            variableEmailTemplatePart.Value.ShouldBe(_value);
        }
    }
}