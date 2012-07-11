namespace EmailMaker.Domain.Emails
{
    public class HtmlEmailPart : EmailPart
    {
        public virtual string Html { get; protected set; }

        protected HtmlEmailPart() {}

        public HtmlEmailPart(string html)
        {
            Html = html;
        }

    }
}