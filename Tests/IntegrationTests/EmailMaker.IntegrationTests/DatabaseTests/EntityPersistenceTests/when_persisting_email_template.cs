using System.Linq;
using EmailMaker.Domain.EmailTemplates;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.EntityPersistenceTests
{
    [TestFixture]
    public class when_persisting_email_template : PersistenceTestBase
    {
        private EmailTemplate _emailTemplate;
        private EmailTemplate _retrievedEmailTemplate;

        public override void PersistenceContext()
        {
            _emailTemplate = new EmailTemplate("html");
            Save(_emailTemplate);
        }

        public override void PersistenceQuery()
        {
            _retrievedEmailTemplate = Get<EmailTemplate>(_emailTemplate.Id);
        }

        [Test]
        public void email_template_correctly_retrieved()
        {
            _retrievedEmailTemplate.Id.ShouldBe(_emailTemplate.Id);
            _retrievedEmailTemplate.Parts.Count().ShouldBe(_emailTemplate.Parts.Count());
            foreach (var retrievedPart in _retrievedEmailTemplate.Parts)
            {
                var htmlRetrievedPart = retrievedPart as HtmlEmailTemplatePart;
                var htmlPart = _emailTemplate.Parts.First(x => x.Id == htmlRetrievedPart.Id) as HtmlEmailTemplatePart;
                htmlRetrievedPart.Position.ShouldBe(htmlPart.Position);
                htmlRetrievedPart.Html.ShouldBe(htmlPart.Html);
            }

        }
    }
}
