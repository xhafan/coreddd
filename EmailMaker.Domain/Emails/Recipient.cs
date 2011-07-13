using Core.Domain;

namespace EmailMaker.Domain.Emails
{
    public class Recipient : Identity<Recipient>, IAggregateRootEntity
    {
        public virtual string EmailAddress { get; private set; }
        public virtual string Name { get; private set; }

        protected Recipient() {}

        public Recipient(string emailAddress, string name) // todo: test missing
        {
            EmailAddress = emailAddress;
            Name = name;
        }
    }
}
