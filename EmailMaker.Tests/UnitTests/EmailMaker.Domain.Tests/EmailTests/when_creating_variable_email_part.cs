using EmailMaker.Domain.Emails;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.Domain.Tests.EmailTests
{
    [TestFixture]
    public class when_creating_variable_email_part
    {
        private VariableEmailPart _emailVariblePart;
        private string _value = "value";

        [SetUp]
        public void Context()
        {
            _emailVariblePart = new VariableEmailPart(VariableType.InputText, _value);
        }

        [Test]
        public void variable_email_part_correctly_created()
        {
            _emailVariblePart.VariableType.ShouldBe(VariableType.InputText);
            _emailVariblePart.Value.ShouldBe(_value);
        }
    }
}