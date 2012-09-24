using CoreDdd.Domain;

namespace EmailMaker.Domain.Emails
{
    public abstract class EmailPart : Entity<EmailPart>
    {
        public virtual int Position { get; protected set; }
    }
}