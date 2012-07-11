using Core.TestHelper.Extensions;
using Core.Utilities.Extensions;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.Emails.EmailStates;
using EmailMaker.Domain.EmailTemplates;
using Iesi.Collections.Generic;

namespace EmailMaker.TestHelper.Builders
{

    public class EmailBuilder
    {       
        private int _nextPartId;
        private int _id;
        private EmailTemplate _emailTemplate;
        private EmailState _state = EmailState.Draft;
        private readonly ISet<Recipient> _recipients = new HashedSet<Recipient>();

        private int NextPartId
        {
            get
            {
                _nextPartId++;
                return _nextPartId;
            }
        }

        public static EmailBuilder New
        {
            get
            {
                return new EmailBuilder();
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

        public EmailBuilder WithRecipient(string emailAddress, string name)
        {
            _recipients.Add(new Recipient(emailAddress, name));
            return this;
        }

        public Email Build()
        {
            var email = new Email(_emailTemplate);
            email.SetPrivateProperty(x => x.Id, _id);
            email.Parts.Each(part => part.SetPrivateProperty(x => x.Id, NextPartId));
            email.SetPrivateProperty("State", _state);
            _recipients.Each(r => email.EmailRecipients.Add(new EmailRecipient(r)));
            return email;
        }
    }
}