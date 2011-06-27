using System.Collections.Generic;
using System.Globalization;
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
    public class when_querying_all_email_template_details : BaseSimplePersistenceTest
    {

        private IEnumerable<EmailTemplateDetailsDTO> _result;
        private EmailTemplate _emailTemplate;
        private CultureInfo english = new CultureInfo("en");
        private CultureInfo japanese = new CultureInfo("ja");

        public override void PersistenceContext()
        {

            var templateNames = new Dictionary<CultureInfo, string>
                                    {
                                        {english , "englishTemplateName"},
                                        {japanese, "japaneseTemplateName"}
                                    };

            _emailTemplate = new EmailTemplate(templateNames);
            Save(_emailTemplate);
        }

        public override void PersistenceQuery()
        {
            var query = new GetAllEmailTemplateQuery();
            _result = query.Execute<EmailTemplateDetailsDTO>(new GetAllEmailTemplateQueryMessage());
        }

        [Test]
        public void email_template_correctly_retrieved()
        {
            _result.Count().ShouldBe(2);
            var retrievedEmailTemplateDTO = _result.First();
            retrievedEmailTemplateDTO.Id.ShouldBe(_emailTemplate.Id);
        }
    }
}