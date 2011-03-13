using Core.Domain;
using EmailMaker.Commands.Handlers;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.EmailTemplates;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace EmailMaker.Commands.Tests.EmailTemplates
{
    [TestFixture]
    public class when_executing_create_email_template_command
    {
        private IRepository<EmailTemplate> _emailTemplateRepository;
        private bool _eventRaised;

        [SetUp]
        public void Context()
        {
            _emailTemplateRepository = MockRepository.GenerateMock<IRepository<EmailTemplate>>();

            var handler = new CreateEmailTemplateCommandHandler(_emailTemplateRepository);
            handler.CommandExecuted += (sender, args) => _eventRaised = true;
            handler.Execute(new CreateEmailTemplateCommand());
        }

        [Test]
        public void email_template_was_saved()
        {
            _emailTemplateRepository.AssertWasCalled(a => a.Save(Arg<EmailTemplate>.Is.NotNull));
        }

        [Test]
        public void event_was_raised()
        {
            _eventRaised.ShouldBe(true);
        }
    
    }
}