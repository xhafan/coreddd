using CoreDdd.Domain.Repositories;
using EmailMaker.Commands.Handlers;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.EmailTemplates;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace EmailMaker.UnitTests.Commands.EmailTemplates
{
    [TestFixture]
    public class when_executing_create_email_template_command
    {
        private IRepository<EmailTemplate> _emailTemplateRepository;
        private bool _eventRaised;
        private const int UserId = 123;

        [SetUp]
        public void Context()
        {
            _emailTemplateRepository = A.Fake<IRepository<EmailTemplate>>();

            var handler = new CreateEmailTemplateCommandHandler(_emailTemplateRepository);
            handler.CommandExecuted += (sender, args) => _eventRaised = true;
            handler.Execute(new CreateEmailTemplateCommand { UserId = UserId });
        }

        [Test]
        public void email_template_was_saved()
        {
            A.CallTo(() => _emailTemplateRepository.Save(A<EmailTemplate>.That.Matches(p => p.UserId == UserId))).MustHaveHappened();
        }

        [Test]
        public void event_was_raised()
        {
            _eventRaised.ShouldBe(true);
        }
    
    }
}