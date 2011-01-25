using System;
using EmailMaker.TestHelper.Builders.EmailTemplates;
using NUnit.Framework;

namespace EmailMaker.Domain.Tests.EmailTemplateTests
{
    [TestFixture]
    public class when_setting_html_for_html_template_part_with_incorrect_html_template_part_id
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Context()
        {
            var emailTemplate = EmailTemplateBuilder.New.Build();
            emailTemplate.SetHtml(-1, string.Empty);
        }
    }
}