using System.Collections.Generic;
using System.Linq;
using Core.TestHelper.Persistence;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.DTO.EmailTemplates;
using EmailMaker.Queries.Handlers;
using EmailMaker.Queries.Messages;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.QueryTests
{
    [TestFixture]
    public class when_querying_email_template : BaseSimplePersistenceTest
    {
        private EmailTemplate _emailTemplate;
        private IEnumerable<EmailTemplateDTO> _result;

        public override void PersistenceContext()
        {
            var user = UserBuilder.New.Build();
            Save(user);
            _emailTemplate = new EmailTemplate("html", "name", user.Id);
            var anotherEmailTemplate = new EmailTemplate("another html", null, user.Id);
            Save(_emailTemplate, anotherEmailTemplate);
        }

        public override void PersistenceQuery()
        {
            var query = new GetEmailTemplateQuery();
            _result = query.Execute<EmailTemplateDTO>(new GetEmailTemplateQueryMessage {EmailTemplateId = _emailTemplate.Id});
        }

        [Test]
        public void email_template_correctly_retrieved()
        {
            _result.Count().ShouldBe(1);
            var retrievedEmailTemplateDTO = _result.First();
            retrievedEmailTemplateDTO.EmailTemplateId.ShouldBe(_emailTemplate.Id);
            retrievedEmailTemplateDTO.Name.ShouldBe(_emailTemplate.Name);
        }
    }
}