namespace EmailMaker.Domain.Emails.EmailStates
{
    public class ToBeSentEmailState : EmailState
    {
        public ToBeSentEmailState() : base(2, "ToBeSent")
        {
            CanSend = false;
        }
    }
}