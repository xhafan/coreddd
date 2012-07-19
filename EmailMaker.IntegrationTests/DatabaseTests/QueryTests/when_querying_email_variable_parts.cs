using System.Collections.Generic;
using System.Linq;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Dtos;
using EmailMaker.Dtos.Emails;
using EmailMaker.Queries.Handlers;
using EmailMaker.Queries.Messages;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.IntegrationTests.DatabaseTests.QueryTests
{
    [TestFixture]
    public class when_querying_email_variable_parts : BaseEmailMakerSimplePersistenceTest
    {
        private IEnumerable<EmailPartDto> _result;
        private Email _email;

        protected override void PersistenceContext()
        {
            var user = UserBuilder.New.Build();
            Save(user);
            var emailTemplate = new EmailTemplate("12345", null, user.Id);
            Save(emailTemplate);
            emailTemplate.CreateVariable(emailTemplate.Parts.First().Id, 1, 1);
            Save(emailTemplate);
            emailTemplate.CreateVariable(emailTemplate.Parts.Last().Id, 1, 1);
            Save(emailTemplate);

            var anotherEmailTemplate = new EmailTemplate("another html", null, user.Id);
            Save(anotherEmailTemplate);

            _email = new Email(emailTemplate);
            Save(_email);
        }

        protected override void PersistenceQuery()
        {
            var query = new GetEmailVariablePartsQuery();
            _result = query.Execute<EmailPartDto>(new GetEmailVariablePartsQueryMessage { EmailId = _email.Id });
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

        private void _VariablePartDTODataMatchVariableEmailPart(EmailPartDto partDto, VariableEmailPart variablePart)
        {
            partDto.EmailId.ShouldBe(_email.Id);
            partDto.PartId.ShouldBe(variablePart.Id);
            partDto.PartType.ShouldBe(PartType.Variable);
            partDto.Html.ShouldBe(null);
            partDto.VariableValue.ShouldBe(variablePart.Value);
        }
    }
}