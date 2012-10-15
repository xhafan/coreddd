using CoreDdd.Domain;

namespace EmailMaker.Domain.Emails
{
    public abstract class EmailPart : Entity
    {
        public virtual int? Position { get; protected set; }
    }
}