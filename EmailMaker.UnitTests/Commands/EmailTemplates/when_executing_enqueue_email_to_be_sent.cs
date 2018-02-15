using System.Collections.Generic;
using System.Linq;
using CoreDdd.Domain.Repositories;
using CoreDdd.Queries;
using EmailMaker.Commands.Handlers;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.Emails;
using EmailMaker.Queries.Messages;
using FakeItEasy;
using NUnit.Framework;

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
            _email = A.Fake<Email>();
            var emailRepository = A.Fake<IRepository<Email>>();
            A.CallTo(() => emailRepository.GetById(emailId)).Returns(_email);

            var recipientParser = A.Fake<IRecipientParser>();
            A.CallTo(() => recipientParser.Parse(Recipients)).Returns(new Dictionary<string, string>{{AddressOne, NameOne}, {AddressTwo, NameTwo}});

            var queryExecutor = A.Fake<IQueryExecutor>();
            A.CallTo(() => queryExecutor.
                    Execute<GetExistingRecipientsQuery, Recipient>(
                    A<GetExistingRecipientsQuery>.That.Matches(p => p.RecipientEmailAddresses.Contains(AddressOne)
                                                                           &&
                                                                           p.RecipientEmailAddresses.Contains(AddressTwo))))
                .Returns(new[]
                            {
                                new Recipient(AddressOne, NameOne)
                            });

            _recipientRepository = A.Fake<IRepository<Recipient>>();

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
            A.CallTo(() => _email.EnqueueEmailToBeSent(A<string>.That.Matches(p => p == FromAddress),
                                                       A<HashSet<Recipient>>.That.Matches(p =>
                                                                                           p.Count == 2
                                                                                           && p.Any(x => x.EmailAddress == AddressOne && x.Name == NameOne)
                                                                                           && p.Any(x => x.EmailAddress == AddressTwo && x.Name == NameTwo)
                                                                                           ),
                                                       A<string>.That.Matches(p => p == Subject)
                )).MustHaveHappened();
        }

        [Test]
        public void only_new_recipients_were_created()
        {
            A.CallTo(() => _recipientRepository.Save(A<Recipient>.That.Matches(p => p.EmailAddress == AddressTwo && p.Name == NameTwo))).MustHaveHappened();
            A.CallTo(() => _recipientRepository.Save(A<Recipient>.That.Matches(p => p.EmailAddress == AddressOne && p.Name == NameOne))).MustNotHaveHappened();
        }

    }
}