namespace EmailMaker.Domain.EmailTemplates
{
    public class HtmlEmailTemplatePart : EmailTemplatePart
    {
        public virtual string Html { get; private set; }

        public HtmlEmailTemplatePart()
        {
            Html = string.Empty;
        }

        public HtmlEmailTemplatePart(string html)
        {
            Html = html;
        }

        public virtual void SetHtml(string html)
        {
            Html = html;
        }
    }
}