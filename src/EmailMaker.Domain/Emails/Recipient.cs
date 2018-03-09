using CoreDdd.Domain;

namespace EmailMaker.Domain.Emails
{
    public class Recipient : Entity, IAggregateRoot
    {
        public virtual string EmailAddress { get; protected set; }
        public virtual string Name { get; protected set; }

        protected Recipient() {}

        public Recipient(string emailAddress, string name)
        {
            EmailAddress = emailAddress;
            Name = name;
        }
    }
}
