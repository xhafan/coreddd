using Core.Domain;

namespace EmailMaker.Domain.EmailTemplates
{
    public abstract class EmailTemplatePart : Entity<EmailTemplatePart>
    {
        public virtual int Position { get; protected set; }
    }

}