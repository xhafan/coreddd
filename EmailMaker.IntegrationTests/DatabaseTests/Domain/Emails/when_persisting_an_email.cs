using System.Linq;
using Core.Tests.Helpers.Persistence;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.Domain.Emails
{
    [TestFixture]
    public class when_persisting_email : BaseSimplePersistenceTest
    {
        private Email _email;
        private Email _retrievedEmail;
        private EmailTemplate _emailTemplate;

        protected override void PersistenceContext()
        {
            var user = UserBuilder.New.Build();
            Save(user);
            _emailTemplate = EmailTemplateBuilder.New
                .WithInitialHtml("123")
                .WithUserId(user.Id)
                .Build();
            Save(_emailTemplate);
            _emailTemplate.CreateVariable(_emailTemplate.Parts.First().Id,  1, 1);
            Save(_emailTemplate);

            _email = new Email(_emailTemplate);
            Save(_email);
        }

        protected override void PersistenceQuery()
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
            var htmlRetrievedPart = (HtmlEmailPart)_retrievedEmail.Parts.ElementAt(0);
            var htmlTemplatePart = (HtmlEmailTemplatePart)_emailTemplate.Parts.ElementAt(0);
            htmlRetrievedPart.Html.ShouldBe(htmlTemplatePart.Html);

            var variableRetrievedPart = (VariableEmailPart)_retrievedEmail.Parts.ElementAt(1);
            var variableTemplatePart = (VariableEmailTemplatePart)_emailTemplate.Parts.ElementAt(1);
            variableRetrievedPart.Value.ShouldBe(variableTemplatePart.Value);

            htmlRetrievedPart = (HtmlEmailPart)_retrievedEmail.Parts.ElementAt(2);
            htmlTemplatePart = (HtmlEmailTemplatePart)_emailTemplate.Parts.ElementAt(2);
            htmlRetrievedPart.Html.ShouldBe(htmlTemplatePart.Html);
        }

        [Test]
        public void email_state()
        {
            _retrievedEmail.State.Name.ShouldBe("Draft");
        }

    }
}