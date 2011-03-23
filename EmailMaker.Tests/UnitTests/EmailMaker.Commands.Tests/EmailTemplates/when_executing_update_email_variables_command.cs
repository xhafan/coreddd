using Core.Domain;
using EmailMaker.Commands.Handlers;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.Emails;
using EmailMaker.DTO.Emails;
using NUnit.Framework;
using Rhino.Mocks;

namespace EmailMaker.Commands.Tests.EmailTemplates
{
    [TestFixture]
    public class when_executing_update_email_variables_command
    {
        private Email _email;
        private EmailDTO _emailDTO;

        [SetUp]
        public void Context()
        {
            _email = MockRepository.GenerateMock<Email>();

            var emailId = 23;
            var emailRepository = MockRepository.GenerateStub<IRepository<Email>>();
            emailRepository.Stub(a => a.GetById(emailId)).Return(_email);

            _emailDTO = new EmailDTO
                                    {
                                        EmailId = emailId,
                                    };
            var command = new UpdateEmailVariablesCommand
                              {
                                  Email = _emailDTO,
                              };
            var handler = new UpdateEmailVariablesCommandHandler(emailRepository);
            handler.Execute(command);
        }

        [Test]
        public void html_and_variable_values_were_set()
        {
            _email.AssertWasCalled(a => a.UpdateVariables(_emailDTO));
        }

    }
}