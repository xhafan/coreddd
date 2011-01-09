namespace EmailMaker.Domain.EmailTemplates
{
    public class HtmlTemplatePart : TemplatePart
    {
        public string Html { get; private set; }

        public HtmlTemplatePart()
        {
            Html = string.Empty;
        }

        public HtmlTemplatePart(string html)
        {
            Html = html;
        }

        public void SetHtml(string html)
        {
            Html = html;
        }
    }
}