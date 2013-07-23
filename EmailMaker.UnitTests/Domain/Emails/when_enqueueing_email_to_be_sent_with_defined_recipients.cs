using CoreUtils;
using EmailMaker.Domain.Emails.EmailStates;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace EmailMaker.UnitTests.Domain.Emails
{
    [TestFixture]
    public class when_enqueueing_email_to_be_sent_with_defined_recipients
    {
        private CoreException _exception;

        [SetUp]
        public void Context()
        {
            var template = EmailTemplateBuilder.New.Build();
            var state = MockRepository.GenerateMock<EmailState>();
            state.Stub(a => a.CanSend).Return(true);
            var email = EmailBuilder.New
                .WithEmailTemplate(template)
                .WithState(state)
                .WithRecipient("email", "name")
                .Build();

            _exception = Should.Throw<CoreException>(() => email.EnqueueEmailToBeSent(null, null, null));
        }

        [Test]
        public void correct_exception_is_thrown()
        {
            _exception.Message.ShouldBe("recipients must be empty");
        }
    }
}