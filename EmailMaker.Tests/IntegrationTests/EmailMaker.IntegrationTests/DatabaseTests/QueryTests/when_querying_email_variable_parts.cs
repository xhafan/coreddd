using System.Collections.Generic;
using System.Linq;
using Core.TestHelper.Persistence;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.DTO;
using EmailMaker.DTO.Emails;
using EmailMaker.Queries.Handlers;
using EmailMaker.Queries.Messages;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.QueryTests
{
    [TestFixture]
    public class when_querying_email_variable_parts : BaseSimplePersistenceTest
    {
        private IEnumerable<EmailPartDTO> _result;
        private Email _email;

        public override void PersistenceContext()
        {
            var emailTemplate = new EmailTemplate("12345");
            Save(emailTemplate);
            emailTemplate.CreateVariable(emailTemplate.Parts.First().Id, 1, 1);
            Save(emailTemplate);
            emailTemplate.CreateVariable(emailTemplate.Parts.Last().Id, 1, 1);
            Save(emailTemplate);

            var anotherEmailTemplate = new EmailTemplate("another html");
            Save(anotherEmailTemplate);

            _email = new Email(emailTemplate);
            Save(_email);
        }

        public override void PersistenceQuery()
        {
            var query = new GetEmailVariablePartsQuery();
            _result = query.Execute<EmailPartDTO>(new GetEmailVariablePartsQueryMessage { EmailId = _email.Id });
        }

        [Test]
        public void email_template_parts_correctly_retrieved()
        {
            _result.Count().ShouldBe(2);

            var variablePart = _email.Parts.ElementAt(1) as VariableEmailPart;
            var partDTO = _result.First();
            _VariablePartDTODataMatchVariableEmailPart(partDTO, variablePart);

            variablePart = _email.Parts.ElementAt(3) as VariableEmailPart;
            partDTO = _result.Last();
            _VariablePartDTODataMatchVariableEmailPart(partDTO, variablePart);
        }

        private void _VariablePartDTODataMatchVariableEmailPart(EmailPartDTO partDTO, VariableEmailPart variablePart)
        {
            partDTO.EmailId.ShouldBe(_email.Id);
            partDTO.PartId.ShouldBe(variablePart.Id);
            partDTO.PartType.ShouldBe(PartType.Variable);
            partDTO.Html.ShouldBe(null);
            partDTO.VariableValue.ShouldBe(variablePart.Value);
        }
    }
}