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
    public class when_querying_email_variable_parts : BaseSimplePersistenceTest
    {
        private IEnumerable<EmailPartDto> _result;
        private Email _email;

        protected override void PersistenceContext()
        {
            var user = UserBuilder.New.Build();
            Save(user);
            var emailTemplate = EmailTemplateBuilder.New
                .WithInitialHtml("12345")
                .WithName(null)
                .WithUserId(user.Id)
                .Build();
            Save(emailTemplate);
            emailTemplate.CreateVariable(emailTemplate.Parts.First().Id, 1, 1);
            Save(emailTemplate);
            emailTemplate.CreateVariable(emailTemplate.Parts.Last().Id, 1, 1);
            Save(emailTemplate);
            var anotherEmailTemplate = EmailTemplateBuilder.New
                .WithInitialHtml("another html")
                .WithName(null)
                .WithUserId(user.Id)
                .Build();
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
            var partDto = _result.First();
            _VariablePartDtoDataMatchVariableEmailPart(partDto, variablePart);

            variablePart = _email.Parts.ElementAt(3) as VariableEmailPart;
            partDto = _result.Last();
            _VariablePartDtoDataMatchVariableEmailPart(partDto, variablePart);
        }

        private void _VariablePartDtoDataMatchVariableEmailPart(EmailPartDto partDto, VariableEmailPart variablePart)
        {
            partDto.EmailId.ShouldBe(_email.Id);
            partDto.PartId.ShouldBe(variablePart.Id);
            partDto.PartType.ShouldBe(PartType.Variable);
            partDto.Html.ShouldBe(null);
            partDto.VariableValue.ShouldBe(variablePart.Value);
        }
    }
}