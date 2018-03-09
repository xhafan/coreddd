using CoreDdd.Domain.Repositories;
using EmailMaker.Commands.Handlers;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Dtos.EmailTemplates;
using FakeItEasy;
using NUnit.Framework;

namespace EmailMaker.UnitTests.Commands.EmailTemplates
{
    [TestFixture]
    public class when_executing_create_variable_command
    {
        private EmailTemplate _emailTemplate;
        private int _htmlTemplatePartId;
        private int _htmlStartIndex;
        private int _length;
        private EmailTemplateDto _emailTemplateDto;

        [SetUp]
        public void Context()
        {
            _emailTemplate = A.Fake<EmailTemplate>();
            
            const int emailTemplateId = 23;
            var emailTemplateRepository = A.Fake<IRepository<EmailTemplate>>();
            A.CallTo(() => emailTemplateRepository.GetById(emailTemplateId)).Returns(_emailTemplate);

            _htmlTemplatePartId = 47;
            _htmlStartIndex = 56;
            _length = 65;
            _emailTemplateDto = new EmailTemplateDto
                                    {
                                        EmailTemplateId = emailTemplateId,
                                    };
            var command = new CreateVariableCommand
                              {
                                  EmailTemplate = _emailTemplateDto,
                                  HtmlStartIndex = _htmlStartIndex,
                                  HtmlTemplatePartId = _htmlTemplatePartId,
                                  Length = _length
                              };
            var handler = new CreateVariableCommandHandler(emailTemplateRepository);
            handler.Execute(command);
        }

        [Test]
        public void html_and_variable_values_were_set()
        {
            A.CallTo(() => _emailTemplate.Update(_emailTemplateDto)).MustHaveHappened();
        }

        [Test]
        public void create_variable_method_was_called()
        {
            A.CallTo(() => _emailTemplate.CreateVariable(_htmlTemplatePartId, _htmlStartIndex, _length)).MustHaveHappened();
        }
    
    }
}
