using System;
using System.Linq;
using NUnit.Framework;
using TestHelper.Builders.EmailTemplates;

namespace EmailMaker.Domain.Tests.EmailTemplateTests
{
    [TestFixture]
    public class when_creating_variable_with_incorrect_string_length
    {
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Context()
        {
            var emailTemplate = EmailTemplateBuilder.New
                .WithFakeIds()
                .WithInitialHtml("html")
                .Build();
            var htmlTeplatePartId = emailTemplate.Parts.First().Id;
            emailTemplate.CreateVariable(htmlTeplatePartId, 1, 4);
        }
    }
}