using Core.Domain;

namespace EmailMaker.Domain.Emails.EmailStates
{
    public abstract class EmailState : Identity<EmailState>
    {              
        public static EmailState Draft = new Draft(1, "Draft");
        public static EmailState ToBeSent = new ToBeSent(2, "ToBeSent");
        public static EmailState Sent = new Sent(3, "Sent");
        
        public virtual string Name { get; private set; }
        public virtual bool CanSend { get; protected set; }

        protected EmailState() { }

        protected EmailState(int id, string name)
        {
            _id = id;
            Name = name;
        }
    }
}