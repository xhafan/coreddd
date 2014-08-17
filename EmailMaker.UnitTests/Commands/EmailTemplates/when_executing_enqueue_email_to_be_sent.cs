using System.Collections.Generic;
using System.Linq;
using CoreDdd.Domain.Repositories;
using CoreDdd.Queries;
using EmailMaker.Commands.Handlers;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.Emails;
using EmailMaker.Queries.Messages;
using NUnit.Framework;
using Rhino.Mocks;

namespace EmailMaker.UnitTests.Commands.EmailTemplates
{
    [TestFixture]
    public class when_executing_enqueue_email_to_be_sent
    {
        private Email _email;
        private const string FromAddress = "from address";
        private const string Subject = "subject";
        private const string AddressOne = "address one";
        private const string AddressTwo = "address two";
        private const string Recipients = "recipients";
        private const string NameOne = "name one";
        private const string NameTwo = "name two";
        private IRepository<Recipient> _recipientRepository;

        [SetUp]
        public void Context()
        {
            const int emailId = 23;
            _email = MockRepository.GenerateMock<Email>();
            var emailRepository = MockRepository.GenerateStub<IRepository<Email>>();
            emailRepository.Stub(a => a.GetById(emailId)).Return(_email);

            var recipientParser = MockRepository.GenerateStub<IRecipientParser>();
            recipientParser.Stub(a => a.Parse(Recipients)).Return(new Dictionary<string, string>{{AddressOne, NameOne}, {AddressTwo, NameTwo}});

            var queryExecutor = MockRepository.GenerateStub<IQueryExecutor>();
            queryExecutor.Stub(
                a =>
                a.Execute<GetExistingRecipientsQuery, Recipient>(
                    Arg<GetExistingRecipientsQuery>.Matches(p => p.RecipientEmailAddresses.Contains(AddressOne)
                                                                           &&
                                                                           p.RecipientEmailAddresses.Contains(AddressTwo))))
                .Return(new[]
                            {
                                new Recipient(AddressOne, NameOne)
                            });

            _recipientRepository = MockRepository.GenerateMock<IRepository<Recipient>>();

            var handler = new EnqueueEmailToBeSentCommandHandler(emailRepository, recipientParser, queryExecutor, _recipientRepository);
            handler.Execute(new EnqueueEmailToBeSentCommand
                                {
                                    EmailId = emailId,
                                    FromAddress = FromAddress,
                                    Recipients = Recipients,
                                    Subject = Subject
                                });
        }

        [Test]
        public void email_enqued_was_called()
        {
            _email.AssertWasCalled(a => a.EnqueueEmailToBeSent(Arg<string>.Matches(p => p == FromAddress),
                                                          Arg<HashSet<Recipient>>.Matches(p =>
                                                                                           p.Count() == 2
                                                                                           && p.Any(x => x.EmailAddress == AddressOne && x.Name == NameOne)
                                                                                           && p.Any(x => x.EmailAddress == AddressTwo && x.Name == NameTwo)
                                                                                           ),
                                                          Arg<string>.Matches(p => p == Subject)));
        }

        [Test]
        public void only_new_recipients_were_created()
        {
            _recipientRepository.AssertWasCalled(a => a.Save(Arg<Recipient>.Matches(p => p.EmailAddress == AddressTwo && p.Name == NameTwo)));
            _recipientRepository.AssertWasNotCalled(a => a.Save(Arg<Recipient>.Matches(p => p.EmailAddress == AddressOne && p.Name == NameOne)));
        }

    }
}