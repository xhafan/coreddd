using System.Collections.Generic;
using System.Linq;
using Core.Tests.Helpers.Persistence;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Dtos;
using EmailMaker.Dtos.EmailTemplates;
using EmailMaker.Queries.Handlers;
using EmailMaker.Queries.Messages;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.Queries
{
    [TestFixture]
    public class when_querying_email_template_parts : base_simple_persistence_test
    {
        private EmailTemplate _emailTemplate;
        private IEnumerable<EmailTemplatePartDto> _result;

        protected override void PersistenceContext()
        {
            var user = UserBuilder.New.Build();
            Save(user);
            _emailTemplate = new EmailTemplate("123", null, user.Id);
            Save(_emailTemplate);
            _emailTemplate.CreateVariable(_emailTemplate.Parts.First().Id, 1, 1);
            var anotherEmailTemplate = new EmailTemplate("another html", null, user.Id);
            Save(_emailTemplate, anotherEmailTemplate);
        }

        protected override void PersistenceQuery()
        {
            var query = new GetEmailTemplatePartsQuery();
            _result = query.Execute<EmailTemplatePartDto>(new GetEmailTemplatePartsQueryMessage { EmailTemplateId = _emailTemplate.Id });
        }

        [Test]
        public void email_template_parts_correctly_retrieved()
        {
            _result.Count().ShouldBe(3);
            
            var htmlPart = (HtmlEmailTemplatePart)_emailTemplate.Parts.First();
            var partDTO = _result.First();
            partDTO.EmailTemplateId.ShouldBe(_emailTemplate.Id);
            partDTO.PartId.ShouldBe(htmlPart.Id);
            partDTO.PartType.ShouldBe(PartType.Html);
            partDTO.Html.ShouldBe(htmlPart.Html);
            partDTO.VariableValue.ShouldBe(null);

            var variablePart = (VariableEmailTemplatePart)_emailTemplate.Parts.ElementAt(1);
            partDTO = _result.ElementAt(1);
            partDTO.EmailTemplateId.ShouldBe(_emailTemplate.Id);
            partDTO.PartId.ShouldBe(variablePart.Id);
            partDTO.PartType.ShouldBe(PartType.Variable);
            partDTO.Html.ShouldBe(null);
            partDTO.VariableValue.ShouldBe(variablePart.Value);

            htmlPart = (HtmlEmailTemplatePart)_emailTemplate.Parts.Last();
            partDTO = _result.Last();
            partDTO.EmailTemplateId.ShouldBe(_emailTemplate.Id);
            partDTO.PartId.ShouldBe(htmlPart.Id);
            partDTO.PartType.ShouldBe(PartType.Html);
            partDTO.Html.ShouldBe(htmlPart.Html);
            partDTO.VariableValue.ShouldBe(null);
        }
    }
}