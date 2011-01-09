namespace EmailMaker.Domain.EmailTemplates
{
    public class EmailTemplate : TemplatePartContainer
    {
        public EmailTemplate()
        {
            Parts.Add(new HtmlTemplatePart());
        }
    }
}
