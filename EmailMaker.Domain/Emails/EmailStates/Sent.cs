namespace EmailMaker.Domain.Emails.EmailStates
{
    internal sealed class Sent : EmailState
    {
        public Sent(int id, string name) : base(id, name)
        {
            CanSend = false;
        }
    }
}