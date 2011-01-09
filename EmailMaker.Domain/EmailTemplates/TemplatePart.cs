using DddCore;

namespace EmailMaker.Domain.EmailTemplates
{
    public abstract class TemplatePart : Identity<TemplatePart>
    {
        public int Position { get; set; }
    }

}