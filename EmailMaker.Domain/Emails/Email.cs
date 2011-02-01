using System.Collections.Generic;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Utilities;


namespace EmailMaker.Domain.Emails
{
    public class Email
    {
        public virtual EmailTemplate EmailTemplate { get; private set; }

        private readonly IList<EmailPart> _parts;

        public virtual IEnumerable<EmailPart> Parts
        {
            get { return _parts; }
        }

        protected Email() {}

        public Email(EmailTemplate emailTemplate)
        {
            _parts = new List<EmailPart>();
            foreach (var emailTemplatePart in emailTemplate.Parts)
            {
                if (emailTemplatePart is HtmlEmailTemplatePart)
                {
                    var htmlEmailTemplatePart = (HtmlEmailTemplatePart) emailTemplatePart;
                    _parts.Add(new HtmlEmailPart(htmlEmailTemplatePart.Html));
                }
                else if (emailTemplatePart is VariableEmailTemplatePart)
                {
                    var variableEmailTemplatePart = (VariableEmailTemplatePart)emailTemplatePart;
                    _parts.Add(new VariableEmailPart(variableEmailTemplatePart.VariableType, variableEmailTemplatePart.Value));                    
                }
                else
                {
                    throw new EmailMakerException("Unsupported email template part: " + emailTemplatePart.GetType());
                }
            }
            
        }
    }
}
