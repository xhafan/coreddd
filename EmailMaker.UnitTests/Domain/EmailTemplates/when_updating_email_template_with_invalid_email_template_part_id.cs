using System;
using EmailMaker.Dtos;
using EmailMaker.Dtos.EmailTemplates;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;

namespace EmailMaker.UnitTests.Domain.EmailTemplates
{
    [TestFixture]
    public class when_updating_email_template_with_invalid_email_template_part_id
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Context()
        {
            var template = EmailTemplateBuilder.New
                .WithId(45)
                .WithInitialHtml("12345")
                .Build();
            var emailTemplateDto = new EmailTemplateDto
                                       {
                                           EmailTemplateId = 45,
                                           Parts = new[] { new EmailTemplatePartDto { PartType = PartType.Html, PartId = 567}}
                                       };
            template.Update(emailTemplateDto);
        }
    }
}