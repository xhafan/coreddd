using System;
using Core.Domain;

namespace EmailMaker.Domain.Emails
{
    public class EmailRecipient : Identity<EmailRecipient>
    {
        public virtual Recipient Recipient { get; private set; }
        public virtual bool Sent { get; private set; }
        public virtual DateTime? SentDate { get; private set; }

        protected EmailRecipient() {}

        public EmailRecipient(Recipient recipient) // todo: test missing
        {
            Recipient = recipient;
        }
    }
}