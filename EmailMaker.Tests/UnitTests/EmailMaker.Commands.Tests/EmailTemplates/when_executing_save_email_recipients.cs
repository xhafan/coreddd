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
    public class when_executing_save_email_recipients
    {
        private Email _email;
        private string _fromAddress = "from address";

        [SetUp]
        public void Context()
        {
            var emailId = 23;
            _email = MockRepository.GenerateMock<Email>();
            var emailRepository = MockRepository.GenerateStub<IRepository<Email>>();
            emailRepository.Stub(a => a.GetById(emailId)).Return(_email);

            var handler = new SaveEmailRecipientsCommandHandler(emailRepository);
            handler.Execute(new SaveEmailRecipientsCommand
                                {
                                    EmailId = emailId,
                                    FromAddress = _fromAddress,
                                    ToAddressesStr = "address1, address2"
                                });
        }

        [Test]
        public void email_from_address_and_recipients_was_saved()
        {
            _email.AssertWasCalled(a => a.SetFromAddressAndRecipients(Arg<string>.Matches(p => p ==_fromAddress), 
                Arg<IEnumerable<string>>.Matches(p => 
                                                    p.Count() == 2
                                                    && p.First() == "address1"
                                                    && p.Last() == "address2"
                                                    )));
        }

    }
}