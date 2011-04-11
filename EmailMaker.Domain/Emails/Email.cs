using System.Collections.Generic;
using System.Linq;
using Core.Domain;
using Core.Utilities;
using Core.Utilities.Extensions;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.DTO;
using EmailMaker.DTO.Emails;
using EmailMaker.Utilities;


namespace EmailMaker.Domain.Emails
{
    public class Email : Identity<Email>, IAggregateRootEntity
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
            EmailTemplate = emailTemplate;
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

        public virtual void UpdateVariables(EmailDTO emailDTO)
        {
            Guard.Hope(Id == emailDTO.EmailId, "Invalid email id");
            emailDTO.Parts.Each(part =>
            {
                if (part.PartType == PartType.Variable)
                {
                    _SetVariableValue(part.PartId, part.VariableValue);
                }
                else
                {
                    throw new EmailMakerException("Unknown email part type: " + part.PartType);
                }
            });
        }

        private VariableEmailPart _GetVariablePart(int variablePartId)
        {
            return (VariableEmailPart)_GetPart(variablePartId);
        }

        private EmailPart _GetPart(int partId)
        {
            return Parts.First(x => x.Id == partId);
        }

        private void _SetVariableValue(int variablePartId, string value)
        {
            _GetVariablePart(variablePartId).SetValue(value);
        }

        public virtual void SetFromAddressAndRecipients(string fromAddress, IEnumerable<string> toAddresses)
        {
            throw new System.NotImplementedException();
        }
    }
}
