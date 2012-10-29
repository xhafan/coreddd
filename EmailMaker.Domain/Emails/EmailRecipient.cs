using System;
using CoreDdd.Domain;

namespace EmailMaker.Domain.Emails
{
    public class EmailRecipient : Entity<long>
    {
        public virtual Email Email { get; protected set; }
        public virtual Recipient Recipient { get; protected set; }
        public virtual bool Sent { get; protected set; }
        public virtual DateTime? SentDate { get; protected set; }

        protected EmailRecipient() {}

        public EmailRecipient(Email email, Recipient recipient)
        {
            Email = email;
            Recipient = recipient;
        }
    }
}