using System;
using System.Linq;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;

namespace EmailMaker.UnitTests.Domain.EmailTemplates
{
    [TestFixture]
    public class when_creating_variable_with_incorrect_string_index
    {
        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Context()
        {
            var emailTemplate = EmailTemplateBuilder.New
                .WithInitialHtml("html")
                .Build();
            var htmlTeplatePartId = emailTemplate.Parts.First().Id;
            emailTemplate.CreateVariable(htmlTeplatePartId, -1, 0);
        }
    }
}