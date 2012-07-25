using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Queries.Handlers;
using EmailMaker.Queries.Messages;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.Queries
{
// todo: Temporarily commented out until templates are multilingual
//    [TestFixture]
//    public class when_querying_all_email_template_details : base_simple_persistence_test
//    {
//
//        private IEnumerable<EmailTemplateDetailsDto> _result;
//        private EmailTemplate _emailTemplate;
//        private CultureInfo english = new CultureInfo("en");
//        private CultureInfo japanese = new CultureInfo("ja");
//
//        public override void PersistenceContext()
//        {
//
//            var templateNames = new Dictionary<CultureInfo, string>
//                                    {
//                                        {english , "englishTemplateName"},
//                                        {japanese, "japaneseTemplateName"}
//                                    };
//
//            _emailTemplate = new EmailTemplate(templateNames);
//            Save(_emailTemplate);
//        }
//
//        public override void PersistenceQuery()
//        {
//            var query = new GetAllEmailTemplateQuery();
//            _result = query.Execute<EmailTemplateDetailsDto>(new GetAllEmailTemplateQueryMessage());
//        }
//
//        [Test]
//        public void email_template_correctly_retrieved()
//        {
//            _result.Count().ShouldBe(2);
//            var retrievedEmailTemplateDto = _result.First();
//            retrievedEmailTemplateDto.EmailTemplateId.ShouldBe(_emailTemplate.Id);
//        }
//    }
}