using CoreDdd.Domain;

namespace EmailMaker.Domain.EmailTemplates
{
    public abstract class EmailTemplatePart : Entity
    {
        public virtual int? Position { get; protected set; }
    }

}