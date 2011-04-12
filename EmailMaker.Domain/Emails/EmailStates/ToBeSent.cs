namespace EmailMaker.Domain.Emails.EmailStates
{
    internal sealed class ToBeSent : EmailState
    {
        public ToBeSent(int id, string name) : base(id, name)
        {
            CanSend = false;
        }
    }
}