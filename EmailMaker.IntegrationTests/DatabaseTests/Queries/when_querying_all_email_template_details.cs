using System.Collections.Generic;
using System.Linq;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Domain.Users;
using EmailMaker.Dtos.EmailTemplates;
using EmailMaker.Queries.Handlers;
using EmailMaker.Queries.Messages;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;
using GetAllEmailTemplateQuery = EmailMaker.Queries.Messages.GetAllEmailTemplateQuery;

namespace EmailMaker.IntegrationTests.DatabaseTests.Queries
{
    [TestFixture]
    public class when_querying_all_email_template_details : BaseEmailMakerSimplePersistenceTest
    {
        private IEnumerable<EmailTemplateDetailsDto> _result;
        private EmailTemplate _emailTemplate;
        private User _user;

        protected override void PersistenceContext()
        {
            _user = UserBuilder.New.Build();
            Save(_user);
            _emailTemplate = EmailTemplateBuilder.New
                .WithInitialHtml("html")
                .WithName("template name")
                .WithUserId(_user.Id)
                .Build(); 
            Save(_emailTemplate);
        }

        protected override void PersistenceQuery()
        {
            var query = new EmailMaker.Queries.Handlers.GetAllEmailTemplateQuery();
            _result = query.Execute<EmailTemplateDetailsDto>(new GetAllEmailTemplateQuery { UserId = _user.Id });
        }

        [Test]
        public void email_template_correctly_retrieved()
        {
            var retrievedEmailTemplateDto = _result.Single();
            retrievedEmailTemplateDto.EmailTemplateId.ShouldBe(_emailTemplate.Id);
        }
    }
}