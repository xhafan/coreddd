using CoreDdd.Utilities;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.Emails.EmailStates;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.Emails
{
    [TestFixture]
    public class when_enqueueing_email_to_be_sent_which_cannot_be_sent
    {
        private Email _email;
        private const string FromAddress = "from address";
        private const string Subject = "subject";
        private CoreException _exception;

        [SetUp]
        public void Context()
        {
            var template = EmailTemplateBuilder.New.Build();
            var state = MockRepository.GenerateMock<EmailState>();
            state.Stub(a => a.CanSend).Return(false);
            state.Stub(a => a.Name).Return("state");
            _email = EmailBuilder.New
                .WithEmailTemplate(template)
                .WithState(state)
                .Build();
            _exception = Should.Throw<CoreException>(() => _email.EnqueueEmailToBeSent(FromAddress, null, Subject));
        }

        [Test]
        public void correct_exception_is_thrown()
        {
            _exception.Message.ShouldBe("cannot enqeue email in the current state: state");
        }
    }
}