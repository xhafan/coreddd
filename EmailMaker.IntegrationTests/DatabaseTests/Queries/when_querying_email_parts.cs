using System.Collections.Generic;
using System.Linq;
using EmailMaker.Domain.Emails;
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
    public class when_querying_email_parts : BaseEmailMakerSimplePersistenceTest
    {
        private IEnumerable<EmailPartDto> _result;
        private Email _email;

        protected override void PersistenceContext()
        {
            var user = UserBuilder.New.Build();
            Save(user);
            var emailTemplate = EmailTemplateBuilder.New
                .WithInitialHtml("123")
                .WithName("template name")
                .WithUserId(user.Id)
                .Build();
            Save(emailTemplate);
            emailTemplate.CreateVariable(emailTemplate.Parts.First().Id, 1, 1);
            Save(emailTemplate);

            var anotherEmailTemplate = EmailTemplateBuilder.New
                .WithInitialHtml("another html")
                .WithName("template name")
                .WithUserId(user.Id)
                .Build(); 
            Save(anotherEmailTemplate);

            _email = new Email(emailTemplate);
            Save(_email);
        }

        protected override void PersistenceQuery()
        {
            var query = new GetEmailPartsQueryHandler();
            _result = query.Execute<EmailPartDto>(new GetEmailPartsQuery { EmailId = _email.Id });
        }

        [Test]
        public void email_template_parts_correctly_retrieved()
        {
            _result.Count().ShouldBe(3);

            var htmlPart = (HtmlEmailPart)_email.Parts.First();
            var partDto = _result.First();
            partDto.EmailId.ShouldBe(_email.Id);
            partDto.PartId.ShouldBe(htmlPart.Id);
            partDto.PartType.ShouldBe(PartType.Html);
            partDto.Html.ShouldBe(htmlPart.Html);
            partDto.VariableValue.ShouldBe(null);

            var variablePart = (VariableEmailPart)_email.Parts.ElementAt(1);
            partDto = _result.ElementAt(1);
            partDto.EmailId.ShouldBe(_email.Id);
            partDto.PartId.ShouldBe(variablePart.Id);
            partDto.PartType.ShouldBe(PartType.Variable);
            partDto.Html.ShouldBe(null);
            partDto.VariableValue.ShouldBe(variablePart.Value);

            htmlPart = (HtmlEmailPart)_email.Parts.Last();
            partDto = _result.Last();
            partDto.EmailId.ShouldBe(_email.Id);
            partDto.PartId.ShouldBe(htmlPart.Id);
            partDto.PartType.ShouldBe(PartType.Html);
            partDto.Html.ShouldBe(htmlPart.Html);
            partDto.VariableValue.ShouldBe(null);
        }
    }
}