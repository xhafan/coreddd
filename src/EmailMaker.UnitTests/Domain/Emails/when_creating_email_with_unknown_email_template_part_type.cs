using System.Collections.Generic;
using CoreTest.Extensions;
using EmailMaker.Core;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Domain.Emails;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.Emails
{
    [TestFixture]
    public class when_creating_email_with_unknown_email_template_part_type
    {
        private class UnknownEmailTemplatePart : EmailTemplatePart {}
                       
        [Test]
        public void Context()
        {
            var template = EmailTemplateBuilder.New
                .WithInitialHtml("123")
                .Build();
            template.SetPrivateAttribute("_parts", new List<EmailTemplatePart> {new UnknownEmailTemplatePart()});
            var ex = Should.Throw<EmailMakerException>(() => { new Email(template); });

            ex.Message.ToLower().ShouldMatch("unsupported.*email.*template.*part");
        }
    }
}