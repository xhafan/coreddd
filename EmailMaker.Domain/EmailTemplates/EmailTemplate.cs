using DddCore;

namespace EmailMaker.Domain.EmailTemplates
{
    public class EmailTemplate : TemplatePartContainer, IAggregateRoot
    {
        public EmailTemplate()
        {
            Parts.Add(new HtmlTemplatePart());
        }
    }
}
