using System.Linq;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Domain.Users;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.EmailTemplatePersistenceTests
{
    [TestFixture]
    public class when_persisting_email_template : BaseEmailMakerSimplePersistenceTest
    {
        private EmailTemplate _emailTemplate;
        private EmailTemplate _retrievedEmailTemplate;
        private string _templateName = "template name";
        private User _user;

        protected override void PersistenceContext()
        {
            _user = UserBuilder.New.Build();
            Save(_user);
            _emailTemplate = new EmailTemplate("html", _templateName, _user.Id);
            Save(_emailTemplate);
        }

        protected override void PersistenceQuery()
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
            _retrievedEmailTemplate.Name.ShouldBe(_templateName);
            _retrievedEmailTemplate.UserId.ShouldBe(_user.Id);
        }
    }
}
