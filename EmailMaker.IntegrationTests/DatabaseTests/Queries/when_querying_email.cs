using System.Collections.Generic;
using System.Linq;
using EmailMaker.Domain.Emails;
using EmailMaker.Dtos.Emails;
using EmailMaker.Queries.Handlers;
using EmailMaker.Queries.Messages;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.Queries
{
    [TestFixture]
    public class when_querying_email : BaseEmailMakerSimplePersistenceTest
    {
        private Email _email;
        private IEnumerable<EmailDto> _result;

        protected override void PersistenceContext()
        {
            var user = UserBuilder.New.Build();
            Save(user);
            var emailTemplate = EmailTemplateBuilder.New
                .WithInitialHtml("html")
                .WithName(null)
                .WithUserId(user.Id)
                .Build(); 
            _email = new Email(emailTemplate);
            var anotherEmailTemplate = EmailTemplateBuilder.New
                .WithInitialHtml("another html")
                .WithName(null)
                .WithUserId(user.Id)
                .Build(); 
            var anotherEmail = new Email(anotherEmailTemplate);

            Save(emailTemplate, _email, anotherEmailTemplate, anotherEmail);
        }

        protected override void PersistenceQuery()
        {
            var query = new GetEmailQuery();
            _result = query.Execute<EmailDto>(new GetEmailQueryMessage { EmailId = _email.Id });
        }

        [Test]
        public void email_template_correctly_retrieved()
        {
            _result.Count().ShouldBe(1);
            var retrievedEmailDto = _result.First();
            retrievedEmailDto.EmailId.ShouldBe(_email.Id);
        }
    }
}