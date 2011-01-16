using DddCore;

namespace EmailMaker.Domain.EmailTemplates
{
    public abstract class EmailTemplatePart : Identity<EmailTemplatePart>
    {
        public virtual int Position { get; private set; }

        public virtual void SetPosition(int position)
        {
            Position = position;
        }
    }

}