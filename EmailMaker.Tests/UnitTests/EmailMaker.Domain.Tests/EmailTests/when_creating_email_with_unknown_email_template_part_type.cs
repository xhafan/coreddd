using System.Collections.Generic;
using Core.TestHelper.Extensions;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.TestHelper.Builders.EmailTemplates;
using EmailMaker.Utilities;
using NUnit.Framework;

namespace EmailMaker.Domain.Tests.EmailTests
{
    [TestFixture]
    public class when_creating_email_with_unknown_email_template_part_type
    {
        private class UnknownEmailTemplatePart : EmailTemplatePart {}
                       
        [Test]
        [ExpectedException(typeof(EmailMakerException), ExpectedMessage = "Unsupported email template part: EmailMaker.Domain.Tests.EmailTests.when_creating_email_with_unknown_email_template_part_type+UnknownEmailTemplatePart")]
        public void Context()
        {
            var template = EmailTemplateBuilder.New
                .WithInitialHtml("123")
                .Build();
            template.SetPrivateAttribute("_parts", new List<EmailTemplatePart> {new UnknownEmailTemplatePart()});
            new Email(template);
        }
    }
}