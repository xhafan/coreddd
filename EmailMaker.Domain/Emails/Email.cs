using System.Collections.Generic;
using System.Linq;
using Core.Domain;
using Core.Domain.Events;
using Core.Utilities;
using Core.Utilities.Extensions;
using EmailMaker.Domain.Emails.EmailStates;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.DTO;
using EmailMaker.DTO.Emails;
using EmailMaker.Domain.Events.Emails;
using EmailMaker.Utilities;

namespace EmailMaker.Domain.Emails
{
    public class Email : Identity<Email>, IAggregateRootEntity
    {
        public virtual EmailTemplate EmailTemplate { get; private set; }
        
        private readonly IList<EmailPart> _parts = new List<EmailPart>();
        public virtual IEnumerable<EmailPart> Parts
        {
            get { return _parts; }
        }

        public virtual string FromAddress { get; private set; }
        public virtual string Subject { get; private set; }
        public virtual EmailState State { get; private set; }
        public virtual IDictionary<string, Recipient> Recipients { get; private set; }

        protected Email() {}

        public Email(EmailTemplate emailTemplate) // todo: test missing
        {
            EmailTemplate = emailTemplate;

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

            State = EmailState.Draft;
            Recipients = new Dictionary<string, Recipient>();            
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

        public virtual void EnqueueEmailToBeSent(string fromAddress, IDictionary<string, Recipient> recipientByEmailAddress, string subject)
        {
            Guard.Hope(State.CanSend, "cannot enqeue email in the current state: " + State.Name);
            State = EmailState.ToBeSent;
            FromAddress = fromAddress;
            Subject = subject;

            var emailAddressesToBeRemoved = Recipients.Keys.Except(recipientByEmailAddress.Keys).ToArray();
            emailAddressesToBeRemoved.Each(emailAddress => Recipients.Remove(emailAddress));
            recipientByEmailAddress.Keys.Each(emailAddress =>
                                     {
                                         if (!Recipients.ContainsKey(emailAddress))
                                         {
                                             Recipients.Add(emailAddress, recipientByEmailAddress[emailAddress]);
                                         }
                                     });

            DomainEvents.RaiseEvent(new EmailEnqueuedToBeSentEvent
                                        {
                                            EmailId = Id
                                        });
        }
    }
}
