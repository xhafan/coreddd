using System.Collections.Generic;
using System.Linq;
using Core.Tests.Helpers.Persistence;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Dtos;
using EmailMaker.Dtos.Emails;
using EmailMaker.Queries.Handlers;
using EmailMaker.Queries.Messages;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.Queries
{
    [TestFixture]
    public class when_querying_email_parts : base_simple_persistence_test
    {
        private IEnumerable<EmailPartDto> _result;
        private Email _email;

        protected override void PersistenceContext()
        {
            var user = UserBuilder.New.Build();
            Save(user);
            var emailTemplate = new EmailTemplate("123", null, user.Id);
            Save(emailTemplate);
            emailTemplate.CreateVariable(emailTemplate.Parts.First().Id, 1, 1);
            Save(emailTemplate);

            var anotherEmailTemplate = new EmailTemplate("another html", null, user.Id);
            Save(anotherEmailTemplate);

            _email = new Email(emailTemplate);
            Save(_email);
        }

        protected override void PersistenceQuery()
        {
            var query = new GetEmailPartsQuery();
            _result = query.Execute<EmailPartDto>(new GetEmailPartsQueryMessage { EmailId = _email.Id });
        }

        [Test]
        public void email_template_parts_correctly_retrieved()
        {
            _result.Count().ShouldBe(3);

            var htmlPart = (HtmlEmailPart)_email.Parts.First();
            var partDTO = _result.First();
            partDTO.EmailId.ShouldBe(_email.Id);
            partDTO.PartId.ShouldBe(htmlPart.Id);
            partDTO.PartType.ShouldBe(PartType.Html);
            partDTO.Html.ShouldBe(htmlPart.Html);
            partDTO.VariableValue.ShouldBe(null);

            var variablePart = (VariableEmailPart)_email.Parts.ElementAt(1);
            partDTO = _result.ElementAt(1);
            partDTO.EmailId.ShouldBe(_email.Id);
            partDTO.PartId.ShouldBe(variablePart.Id);
            partDTO.PartType.ShouldBe(PartType.Variable);
            partDTO.Html.ShouldBe(null);
            partDTO.VariableValue.ShouldBe(variablePart.Value);

            htmlPart = (HtmlEmailPart)_email.Parts.Last();
            partDTO = _result.Last();
            partDTO.EmailId.ShouldBe(_email.Id);
            partDTO.PartId.ShouldBe(htmlPart.Id);
            partDTO.PartType.ShouldBe(PartType.Html);
            partDTO.Html.ShouldBe(htmlPart.Html);
            partDTO.VariableValue.ShouldBe(null);
        }
    }
}