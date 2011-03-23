using EmailMaker.Domain.Emails;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.Domain.Tests.EmailTests
{
    [TestFixture]
    public class when_setting_value_for_variable_email_part
    {
        private VariableEmailPart _emailVariblePart;
        private string _newValue = "new value";

        [SetUp]
        public void Context()
        {
            _emailVariblePart = new VariableEmailPart(VariableType.InputText, "value");
            _emailVariblePart.SetValue(_newValue);
        }

        [Test]
        public void variable_email_part_correctly_created()
        {
            _emailVariblePart.Value.ShouldBe(_newValue);
        }
    }
}