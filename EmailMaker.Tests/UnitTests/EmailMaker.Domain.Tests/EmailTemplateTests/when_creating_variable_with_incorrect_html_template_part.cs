using System;
using EmailMaker.TestHelper.Builders.EmailTemplates;
using NUnit.Framework;

namespace EmailMaker.Domain.Tests.EmailTemplateTests
{
    [TestFixture]
    public class when_creating_variable_with_incorrect_html_template_part
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Context()
        {
            var emailTemplate = EmailTemplateBuilder.New.Build();
            emailTemplate.CreateVariable(-1, 0, 0);
        }
    }
}