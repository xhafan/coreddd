using DddCore;

namespace EmailMaker.Domain.Emails
{
    public abstract class EmailPart : Identity<EmailPart>
    {
        public virtual int Position { get; private set; }
    }
}