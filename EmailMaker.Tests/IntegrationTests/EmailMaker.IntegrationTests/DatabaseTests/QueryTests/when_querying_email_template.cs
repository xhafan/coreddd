using System.Collections.Generic;
using System.Linq;
using Core.TestHelper.Persistence;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.DTO.EmailTemplates;
using EmailMaker.Queries.Handlers;
using EmailMaker.Queries.Messages;
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
            _emailTemplate = new EmailTemplate("html");
            var anotherEmailTemplate = new EmailTemplate("another html");
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
        }
    }
}