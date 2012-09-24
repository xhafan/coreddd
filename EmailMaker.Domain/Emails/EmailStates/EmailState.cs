using CoreDdd.Domain;

namespace EmailMaker.Domain.Emails.EmailStates
{
    public abstract class EmailState : Entity<EmailState>
    {              
        public static EmailState Draft = new DraftEmailState();
        public static EmailState ToBeSent = new ToBeSentEmailState();
        public static EmailState Sent = new SentEmailState();

        public virtual string Name { get; protected set; }
        public virtual bool CanSend { get; protected set; }

        protected EmailState() { }

        protected EmailState(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}