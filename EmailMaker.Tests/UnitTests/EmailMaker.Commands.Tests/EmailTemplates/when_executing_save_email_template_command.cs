using Core.Domain;
using EmailMaker.Commands.Handlers;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.DTO.EmailTemplate;
using NUnit.Framework;
using Rhino.Mocks;

namespace EmailMaker.Commands.Tests.EmailTemplates
{
    [TestFixture]
    public class when_executing_save_email_template_command
    {
        private EmailTemplate _emailTemplate;
        private EmailTemplateDTO _emailTemplateDTO;

        [SetUp]
        public void Context()
        {
            _emailTemplate = MockRepository.GenerateMock<EmailTemplate>();

            var emailTemplateId = 23;
            var emailTemplateRepository = MockRepository.GenerateStub<IRepository<EmailTemplate>>();
            emailTemplateRepository.Stub(a => a.GetById(emailTemplateId)).Return(_emailTemplate);

            _emailTemplateDTO = new EmailTemplateDTO
                                    {
                                        EmailTemplateId = emailTemplateId,
                                    };
            var command = new SaveEmailTemplateCommand
                              {
                                  EmailTemplate = _emailTemplateDTO,
                              };
            var handler = new SaveEmailTemplateCommandHandler(emailTemplateRepository);
            handler.Execute(command);
        }

        [Test]
        public void html_and_variable_values_were_set()
        {
            _emailTemplate.AssertWasCalled(a => a.Update(_emailTemplateDTO));
        }

    }
}