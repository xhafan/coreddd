namespace EmailMaker.Domain.Emails.EmailStates
{
    public class DraftEmailState : EmailState
    {
        public DraftEmailState() : base(1, "Draft")
        {
            CanSend = true;
        }
    }
}