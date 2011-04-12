namespace EmailMaker.Domain.Emails.EmailStates
{
    internal sealed class Draft : EmailState
    {
        public Draft(int id, string name) : base(id, name)
        {
            CanSend = true;
        }
    }
}