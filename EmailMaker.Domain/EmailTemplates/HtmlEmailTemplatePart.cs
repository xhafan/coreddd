namespace EmailMaker.Domain.EmailTemplates
{
    public class HtmlEmailTemplatePart : EmailTemplatePart
    {
        public virtual string Html { get; private set; }

        public HtmlEmailTemplatePart()
        {
            _SetHtml(string.Empty);
        }

        public HtmlEmailTemplatePart(string html)
        {
            _SetHtml(html);
        }

        public virtual void SetHtml(string html)
        {
            _SetHtml(html);
        }

        private void _SetHtml(string html)
        {
            Html = html ?? string.Empty;
        }
    }
}