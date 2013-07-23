using System.Linq;
using CoreUtils.Extensions;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Domain.Users;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.Domain.EmailTemplates
{
    [TestFixture]
    public class when_persisting_email_template : BaseEmailMakerSimplePersistenceTest
    {
        private EmailTemplate _emailTemplate;
        private EmailTemplate _retrievedEmailTemplate;
        private const string TemplateName = "template name";
        private User _user;

        protected override void PersistenceContext()
        {
            _user = UserBuilder.New.Build();
            Save(_user);
            _emailTemplate = EmailTemplateBuilder.New
                .WithInitialHtml("html")
                .WithName(TemplateName)
                .WithUserId(_user.Id)
                .Build();
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
            _retrievedEmailTemplate.Name.ShouldBe(TemplateName);
            _retrievedEmailTemplate.UserId.ShouldBe(_user.Id);
            _retrievedEmailTemplate.Parts.Count().ShouldBe(_emailTemplate.Parts.Count());
            _retrievedEmailTemplate.Parts.Each((i, x) =>
                {
                    var htmlRetrievedPart = (HtmlEmailTemplatePart)x;
                    var htmlPart = (HtmlEmailTemplatePart) _emailTemplate.Parts.ElementAt(i);
                    htmlRetrievedPart.ShouldBe(htmlPart);
                    htmlRetrievedPart.Html.ShouldBe(htmlPart.Html);
                });
        }
    }
}
