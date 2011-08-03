namespace EmailMaker.Domain.Emails.EmailStates
{
    public class SentEmailState : EmailState
    {
        public SentEmailState() : base(3, "Sent")
        {
            CanSend = false;
        }
    }
}