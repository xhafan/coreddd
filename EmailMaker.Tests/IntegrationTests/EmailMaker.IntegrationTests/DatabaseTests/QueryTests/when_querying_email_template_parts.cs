using System.Collections.Generic;
using System.Linq;
using Core.TestHelper.Persistence;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.DTO;
using EmailMaker.DTO.EmailTemplates;
using EmailMaker.Queries.Handlers;
using EmailMaker.Queries.Messages;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.QueryTests
{
    [TestFixture]
    public class when_querying_email_template_parts : BaseSimplePersistenceTest
    {
        private EmailTemplate _emailTemplate;
        private IEnumerable<EmailTemplatePartDTO> _result;

        public override void PersistenceContext()
        {
            var user = UserBuilder.New.Build();
            Save(user);
            _emailTemplate = new EmailTemplate("123", null, user.Id);
            Save(_emailTemplate);
            _emailTemplate.CreateVariable(_emailTemplate.Parts.First().Id, 1, 1);
            var anotherEmailTemplate = new EmailTemplate("another html", null, user.Id);
            Save(_emailTemplate, anotherEmailTemplate);
        }

        public override void PersistenceQuery()
        {
            var query = new GetEmailTemplatePartsQuery();
            _result = query.Execute<EmailTemplatePartDTO>(new GetEmailTemplatePartsQueryMessage { EmailTemplateId = _emailTemplate.Id });
        }

        [Test]
        public void email_template_parts_correctly_retrieved()
        {
            _result.Count().ShouldBe(3);
            
            var htmlPart = _emailTemplate.Parts.First() as HtmlEmailTemplatePart;
            var partDTO = _result.First();
            partDTO.EmailTemplateId.ShouldBe(_emailTemplate.Id);
            partDTO.PartId.ShouldBe(htmlPart.Id);
            partDTO.PartType.ShouldBe(PartType.Html);
            partDTO.Html.ShouldBe(htmlPart.Html);
            partDTO.VariableValue.ShouldBe(null);

            var variablePart = _emailTemplate.Parts.ElementAt(1) as VariableEmailTemplatePart;
            partDTO = _result.ElementAt(1);
            partDTO.EmailTemplateId.ShouldBe(_emailTemplate.Id);
            partDTO.PartId.ShouldBe(variablePart.Id);
            partDTO.PartType.ShouldBe(PartType.Variable);
            partDTO.Html.ShouldBe(null);
            partDTO.VariableValue.ShouldBe(variablePart.Value);

            htmlPart = _emailTemplate.Parts.Last() as HtmlEmailTemplatePart;
            partDTO = _result.Last();
            partDTO.EmailTemplateId.ShouldBe(_emailTemplate.Id);
            partDTO.PartId.ShouldBe(htmlPart.Id);
            partDTO.PartType.ShouldBe(PartType.Html);
            partDTO.Html.ShouldBe(htmlPart.Html);
            partDTO.VariableValue.ShouldBe(null);
        }
    }
}