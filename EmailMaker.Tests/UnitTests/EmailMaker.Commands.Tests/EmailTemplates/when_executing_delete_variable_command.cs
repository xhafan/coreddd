using Core.Domain;
using Core.Utilities.Extensions;
using EmailMaker.Commands.Handlers;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.DTO.EmailTemplate;
using NUnit.Framework;
using Rhino.Mocks;

namespace EmailMaker.Commands.Tests.EmailTemplates
{
    [TestFixture]
    public class when_executing_delete_variable_command
    {
        private EmailTemplate _emailTemplate;
        private EmailTemplatePartDTO[] _emailTemplatePartDtos;
        private int _variablePartId;

        [SetUp]
        public void Context()
        {
            _emailTemplate = MockRepository.GenerateMock<EmailTemplate>();

            var emailTemplateId = 23;
            var emailTemplateRepository = MockRepository.GenerateStub<IRepository<EmailTemplate>>();
            emailTemplateRepository.Stub(a => a.GetById(emailTemplateId)).Return(_emailTemplate);

            _variablePartId = 46;
            _emailTemplatePartDtos = new[]
                                         {
                                             new EmailTemplatePartDTO {EmailTemplatePartType = EmailTemplatePartType.Html, PartId = 45, Html = "html1"},
                                             new EmailTemplatePartDTO {EmailTemplatePartType = EmailTemplatePartType.Variable, PartId = _variablePartId, VariableValue = "value1"},
                                             new EmailTemplatePartDTO {EmailTemplatePartType = EmailTemplatePartType.Html, PartId = 47, Html = "html2"},
                                         };
            var command = new DeleteVariableCommand
                              {
                                  VariablePartId = _variablePartId,
                                  EmailTemplate =
                                      new EmailTemplateDTO
                                          {
                                              EmailTemplateId = emailTemplateId,
                                              Parts =
                                                  _emailTemplatePartDtos
                                          },
                              };
            var handler = new DeleteVariableCommandHandler(emailTemplateRepository);
            handler.Execute(command);
        }

        [Test]
        public void html_and_variable_values_were_set()
        {
            _emailTemplatePartDtos.Each(part =>
                                            {
                                                if (part.EmailTemplatePartType == EmailTemplatePartType.Html)
                                                {
                                                    _emailTemplate.AssertWasCalled(a => a.SetHtml(part.PartId, part.Html));
                                                }
                                                else if (part.EmailTemplatePartType == EmailTemplatePartType.Variable)
                                                {
                                                    _emailTemplate.AssertWasCalled(a => a.SetVariableValue(part.PartId, part.VariableValue));
                                                }

                                            });
        }

        [Test]
        public void create_variable_method_was_called()
        {
            _emailTemplate.AssertWasCalled(a => a.DeleteVariable(_variablePartId));
        }

    }
}