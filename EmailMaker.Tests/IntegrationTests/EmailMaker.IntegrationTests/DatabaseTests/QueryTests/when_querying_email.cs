using System.Collections.Generic;
using System.Linq;
using Core.TestHelper.Persistence;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.DTO.Emails;
using EmailMaker.Queries.Handlers;
using EmailMaker.Queries.Messages;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.QueryTests
{
    [TestFixture]
    public class when_querying_email : BaseSimplePersistenceTest
    {
        private Email _email;
        private IEnumerable<EmailDTO> _result;

        public override void PersistenceContext()
        {
            var emailTemplate = new EmailTemplate("html");
            _email = new Email(emailTemplate);

            var anotherEmailTemplate = new EmailTemplate("another html");
            var anotherEmail = new Email(anotherEmailTemplate);

            Save(emailTemplate, _email, anotherEmailTemplate, anotherEmail);
        }

        public override void PersistenceQuery()
        {
            var query = new GetEmailQuery();
            _result = query.Execute<EmailDTO>(new GetEmailQueryMessage { EmailId = _email.Id });
        }

        [Test]
        public void email_template_correctly_retrieved()
        {
            _result.Count().ShouldBe(1);
            var retrievedEmailDTO = _result.First();
            retrievedEmailDTO.EmailId.ShouldBe(_email.Id);
        }
    }
}