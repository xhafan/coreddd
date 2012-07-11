using Core.Domain;

namespace EmailMaker.Domain.Emails
{
    public abstract class EmailPart : Identity<EmailPart>
    {
        public virtual int Position { get; protected set; }
    }
}