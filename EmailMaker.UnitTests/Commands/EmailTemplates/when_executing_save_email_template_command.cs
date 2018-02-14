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
    public class when_executing_save_email_template_command
    {
        private EmailTemplate _emailTemplate;
        private EmailTemplateDto _emailTemplateDto;

        [SetUp]
        public void Context()
        {
            _emailTemplate = A.Fake<EmailTemplate>();

            const int emailTemplateId = 23;
            var emailTemplateRepository = A.Fake<IRepository<EmailTemplate>>();
            A.CallTo(() => emailTemplateRepository.GetById(emailTemplateId)).Returns(_emailTemplate);

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
            A.CallTo(() => _emailTemplate.Update(_emailTemplateDto)).MustHaveHappened();
        }

    }
}