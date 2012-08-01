using System;
using Core.Domain;

namespace EmailMaker.Domain.Emails
{
    public class EmailRecipient : Identity<EmailRecipient>
    {
        public virtual Recipient Recipient { get; protected set; }
        public virtual bool Sent { get; protected set; }
        public virtual DateTime? SentDate { get; protected set; }

        protected EmailRecipient() {}

        public EmailRecipient(Recipient recipient)
        {
            Recipient = recipient;
        }
    }
}