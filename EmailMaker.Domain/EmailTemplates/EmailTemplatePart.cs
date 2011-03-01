using Core.Domain;

namespace EmailMaker.Domain.EmailTemplates
{
    public abstract class EmailTemplatePart : Identity<EmailTemplatePart>
    {
        public virtual int Position { get; private set; }
    }

}