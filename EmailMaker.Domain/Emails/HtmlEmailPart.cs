namespace EmailMaker.Domain.Emails
{
    public class HtmlEmailPart : EmailPart
    {
        public virtual string Html { get; private set; }

        public HtmlEmailPart(string html)
        {
            Html = html;
        }

    }
}