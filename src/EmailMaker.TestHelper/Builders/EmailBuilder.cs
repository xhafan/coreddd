using System.Collections.Generic;
using CoreTest.Extensions;
using CoreUtils.Extensions;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.Emails.EmailStates;
using EmailMaker.Domain.EmailTemplates;

namespace EmailMaker.TestHelper.Builders
{
    public class EmailBuilder
    {
        private int _nextPartId;
        private int _id;
        private EmailTemplate _emailTemplate;
        private EmailState _state = EmailState.Draft;
        private readonly ICollection<Recipient> _recipients = new List<Recipient>();
        private string _fromAddress;
        private string _subject;
        private bool _assignIdsToParts = true;

        private int NextPartId
        {
            get
            {
                _nextPartId++;
                return _nextPartId;
            }
        }

        public EmailBuilder WithEmailTemplate(EmailTemplate emailTemplate)
        {
            _emailTemplate = emailTemplate;
            return this;
        }

        public EmailBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public EmailBuilder WithState(EmailState state)
        {
            _state = state;
            return this;
        }

        public EmailBuilder WithRecipient(Recipient recipient)
        {
            _recipients.Add(recipient);
            return this;
        }

        public EmailBuilder WithFromAddress(string fromAddress)
        {
            _fromAddress = fromAddress;
            return this;
        }

        public EmailBuilder WithSubject(string subject)
        {
            _subject = subject;
            return this;
        }

        public EmailBuilder WithoutAssigningIdsToParts()
        {
            _assignIdsToParts = false;
            return this;
        }

        public Email Build()
        {
            var email = new Email(_emailTemplate);
            email.SetPrivateProperty(x => x.Id, _id);
            if (_assignIdsToParts) email.Parts.Each(part => part.SetPrivateProperty(x => x.Id, NextPartId));
            email.SetPrivateProperty(x => x.State, _state);
            email.SetPrivateProperty(x => x.FromAddress, _fromAddress);
            email.SetPrivateProperty(x => x.Subject, _subject);
            var emailRecipients = (ICollection<EmailRecipient>)email.EmailRecipients;
            _recipients.Each(r => emailRecipients.Add(new EmailRecipient(email, r)));
            return email;
        }
    }
}