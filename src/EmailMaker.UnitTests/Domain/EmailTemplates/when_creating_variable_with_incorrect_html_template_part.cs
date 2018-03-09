using System;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.EmailTemplates
{
    [TestFixture]
    public class when_creating_variable_with_incorrect_html_template_part
    {
        private InvalidOperationException _exception;

        [SetUp]
        public void Context()
        {
            var emailTemplate = EmailTemplateBuilder.New.Build();
            _exception = Should.Throw<InvalidOperationException>(() => emailTemplate.CreateVariable(-1, 0, 0));
        }

        [Test]
        public void exception_was_thrown()
        {
            _exception.Message.ToLower().ShouldMatch("sequence contains no matching element");
        }
    }
}