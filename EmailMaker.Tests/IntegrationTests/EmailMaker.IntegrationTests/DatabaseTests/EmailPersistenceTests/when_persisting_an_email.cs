using System.Linq;
using Core.TestHelper.Persistence;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.EmailTemplates;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.EmailPersistenceTests
{
    [TestFixture]
    public class when_persisting_email : BaseSimplePersistenceTest
    {
        private Email _email;
        private Email _retrievedEmail;
        private EmailTemplate _emailTemplate;

        public override void PersistenceContext()
        {
            _emailTemplate = new EmailTemplate("123");
            Save(_emailTemplate);
            _emailTemplate.CreateVariable(_emailTemplate.Parts.First().Id,  1, 1);
            Save(_emailTemplate);

            _email = new Email(_emailTemplate);
            Save(_email);
        }

        public override void PersistenceQuery()
        {
            _retrievedEmail = Get<Email>(_email.Id);
        }

        [Test]
        public void email_correctly_retrieved()
        {
            _retrievedEmail.Id.ShouldBe(_email.Id);
            _retrievedEmail.EmailTemplate.ShouldBe(_emailTemplate);
            _retrievedEmail.Parts.Count().ShouldBe(_emailTemplate.Parts.Count());
        }

        [Test]
        public void email_parts_correctly_retrieved()
        {
            var htmlRetrievedPart = _retrievedEmail.Parts.ElementAt(0) as HtmlEmailPart;
            var htmlTemplatePart = _emailTemplate.Parts.ElementAt(0) as HtmlEmailTemplatePart;
            htmlRetrievedPart.Html.ShouldBe(htmlTemplatePart.Html);

            var variableRetrievedPart = _retrievedEmail.Parts.ElementAt(1) as VariableEmailPart;
            var variableTemplatePart = _emailTemplate.Parts.ElementAt(1) as VariableEmailTemplatePart;
            variableRetrievedPart.Value.ShouldBe(variableTemplatePart.Value);

            htmlRetrievedPart = _retrievedEmail.Parts.ElementAt(2) as HtmlEmailPart;
            htmlTemplatePart = _emailTemplate.Parts.ElementAt(2) as HtmlEmailTemplatePart;
            htmlRetrievedPart.Html.ShouldBe(htmlTemplatePart.Html);
        }
    }
}