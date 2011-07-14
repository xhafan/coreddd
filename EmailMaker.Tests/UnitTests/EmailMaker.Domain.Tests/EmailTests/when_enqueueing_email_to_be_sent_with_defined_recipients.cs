using Core.Utilities;
using EmailMaker.Domain.Emails.EmailStates;
using EmailMaker.TestHelper.Builders;
using NUnit.Framework;
using Rhino.Mocks;

namespace EmailMaker.Domain.Tests.EmailTests
{
    [TestFixture]
    public class when_enqueueing_email_to_be_sent_with_defined_recipients
    {
        [Test]
        [ExpectedException(typeof(CoreException), ExpectedMessage = "recipients must be empty")]
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
            email.EnqueueEmailToBeSent(null, null, null);
        }
    }
}