using Core.Domain;
using EmailMaker.Commands.Handlers;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.DTO.EmailTemplate;
using EmailMaker.Utilities;
using NUnit.Framework;
using Rhino.Mocks;

namespace EmailMaker.Commands.Tests.EmailTemplates
{
    [TestFixture]
    public class when_executing_create_variable_command_with_incorrect_email_template_structure
    {
        private EmailTemplate _emailTemplate;
        private EmailTemplatePartDTO[] _emailTemplatePartDtos;

        [Test]
        [ExpectedException(typeof(EmailMakerException), ExpectedMessage = "Unknown email template part, email template id: 23")]
        public void Context()
        {
            _emailTemplate = MockRepository.GenerateMock<EmailTemplate>();

            var emailTemplateId = 23;
            var emailTemplateRepository = MockRepository.GenerateStub<IRepository<EmailTemplate>>();
            emailTemplateRepository.Stub(a => a.GetById(emailTemplateId)).Return(_emailTemplate);

            _emailTemplatePartDtos = new[]
                                         {
                                             new EmailTemplatePartDTO {PartId = 45}
                                         };
            var command = new CreateVariableCommand
                              {
                                  EmailTemplate =
                                      new EmailTemplateDTO
                                          {
                                              EmailTemplateId = emailTemplateId,
                                              Parts = _emailTemplatePartDtos
                                          },
                              };
            var handler = new CreateVariableCommandHandler(emailTemplateRepository);
            handler.Execute(command);
        }
    }
}