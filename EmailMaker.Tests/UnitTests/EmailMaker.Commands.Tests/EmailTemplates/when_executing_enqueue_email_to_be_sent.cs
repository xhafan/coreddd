using System.Collections.Generic;
using System.Linq;
using Core.Domain;
using EmailMaker.Commands.Handlers;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.Emails;
using NUnit.Framework;
using Rhino.Mocks;

namespace EmailMaker.Commands.Tests.EmailTemplates
{
    [TestFixture]
    public class when_executing_enqueue_email_to_be_sent
    {
        private Email _email;
        private string _fromAddress = "from address";
        private string _subject = "subject";

        [SetUp]
        public void Context()
        {
            var emailId = 23;
            _email = MockRepository.GenerateMock<Email>();
            var emailRepository = MockRepository.GenerateStub<IRepository<Email>>();
            emailRepository.Stub(a => a.GetById(emailId)).Return(_email);

            var handler = new EnqueueEmailToBeSentCommandHandler(emailRepository);
            handler.Execute(new EnqueueEmailToBeSentCommand
                                {
                                    EmailId = emailId,
                                    FromAddress = _fromAddress,
                                    RecipientsStr = "address1, address2",
                                    Subject = _subject
                                });
        }

        [Test]
        public void email_from_address_and_recipients_was_saved()
        {
            throw new System.NotImplementedException();
            _email.AssertWasCalled(a => a.EnqueueEmailToBeSent(Arg<string>.Matches(p => p == _fromAddress),
                                                          Arg<IEnumerable<string>>.Matches(p =>
                                                                                           p.Count() == 2
                                                                                           && p.First() == "address1"
                                                                                           && p.Last() == "address2"
                                                                                           ),
                                                          Arg<string>.Matches(p => p == _subject)));
        }

    }
}