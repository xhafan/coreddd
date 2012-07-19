using System.Collections.Generic;
using System.Linq;
using Core.Domain;
using Core.Queries;
using EmailMaker.Commands.Handlers;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.Emails;
using EmailMaker.Queries.Messages;
using Iesi.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;

namespace EmailMaker.UnitTests.Commands.EmailTemplates
{
    [TestFixture]
    public class when_executing_enqueue_email_to_be_sent
    {
        private Email _email;
        private string _fromAddress = "from address";
        private string _subject = "subject";
        private string address1 = "address1";
        private string address2 = "address2";
        private string _recipientsStr = "recipientStr";
        private string _name1 = "name1";
        private string _name2 = "name2";
        private IRepository<Recipient> _recipientRepository;

        [SetUp]
        public void Context()
        {
            var emailId = 23;
            _email = MockRepository.GenerateMock<Email>();
            var emailRepository = MockRepository.GenerateStub<IRepository<Email>>();
            emailRepository.Stub(a => a.GetById(emailId)).Return(_email);

            var recipientParser = MockRepository.GenerateStub<IRecipientParser>();
            recipientParser.Stub(a => a.Parse(_recipientsStr)).Return(new Dictionary<string, string>{{address1, _name1}, {address2, _name2}});

            var queryExecutor = MockRepository.GenerateStub<IQueryExecutor>();
            queryExecutor.Stub(
                a =>
                a.Execute<GetExistingRecipientsQueryMessage, Recipient>(
                    Arg<GetExistingRecipientsQueryMessage>.Matches(p => p.RecipientEmailAddresses.Contains(address1)
                                                                           &&
                                                                           p.RecipientEmailAddresses.Contains(address2))))
                .Return(new[]
                            {
                                new Recipient(address1, _name1)
                            });

            _recipientRepository = MockRepository.GenerateMock<IRepository<Recipient>>();

            var handler = new EnqueueEmailToBeSentCommandHandler(emailRepository, recipientParser, queryExecutor, _recipientRepository);
            handler.Execute(new EnqueueEmailToBeSentCommand
                                {
                                    EmailId = emailId,
                                    FromAddress = _fromAddress,
                                    RecipientsStr = _recipientsStr,
                                    Subject = _subject
                                });
        }

        [Test]
        public void email_enqued_was_called()
        {
            _email.AssertWasCalled(a => a.EnqueueEmailToBeSent(Arg<string>.Matches(p => p == _fromAddress),
                                                          Arg<HashedSet<Recipient>>.Matches(p =>
                                                                                           p.Count() == 2
                                                                                           && p.Any(x => x.EmailAddress == address1 && x.Name == _name1)
                                                                                           && p.Any(x => x.EmailAddress == address2 && x.Name == _name2)
                                                                                           ),
                                                          Arg<string>.Matches(p => p == _subject)));
        }

        [Test]
        public void only_new_recipients_were_created()
        {
            _recipientRepository.AssertWasCalled(a => a.Save(Arg<Recipient>.Matches(p => p.EmailAddress == address2 && p.Name == _name2)));
            _recipientRepository.AssertWasNotCalled(a => a.Save(Arg<Recipient>.Matches(p => p.EmailAddress == address1 && p.Name == _name1)));
        }

    }
}