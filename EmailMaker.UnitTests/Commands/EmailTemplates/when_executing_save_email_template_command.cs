using Core.Domain.Repositories;
using EmailMaker.Commands.Handlers;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Dtos.EmailTemplates;
using NUnit.Framework;
using Rhino.Mocks;

namespace EmailMaker.UnitTests.Commands.EmailTemplates
{
    [TestFixture]
    public class when_executing_save_email_template_command
    {
        private EmailTemplate _emailTemplate;
        private EmailTemplateDto _emailTemplateDto;

        [SetUp]
        public void Context()
        {
            _emailTemplate = MockRepository.GenerateMock<EmailTemplate>();

            const int emailTemplateId = 23;
            var emailTemplateRepository = MockRepository.GenerateStub<IRepository<EmailTemplate>>();
            emailTemplateRepository.Stub(a => a.GetById(emailTemplateId)).Return(_emailTemplate);

            _emailTemplateDto = new EmailTemplateDto
                                    {
                                        EmailTemplateId = emailTemplateId,
                                    };
            var command = new SaveEmailTemplateCommand
                              {
                                  EmailTemplate = _emailTemplateDto,
                              };
            var handler = new SaveEmailTemplateCommandHandler(emailTemplateRepository);
            handler.Execute(command);
        }

        [Test]
        public void html_and_variable_values_were_set()
        {
            _emailTemplate.AssertWasCalled(a => a.Update(_emailTemplateDto));
        }

    }
}