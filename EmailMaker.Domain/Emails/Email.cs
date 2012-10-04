using System.Collections.Generic;
using System.Linq;
using CoreDdd.Domain;
using CoreDdd.Domain.Events;
using CoreDdd.Utilities;
using CoreDdd.Utilities.Extensions;
using EmailMaker.Core;
using EmailMaker.Domain.Emails.EmailStates;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Domain.Events.Emails;
using EmailMaker.Dtos;
using EmailMaker.Dtos.Emails;
using Iesi.Collections.Generic;

namespace EmailMaker.Domain.Emails
{
    public class Email : Entity, IAggregateRoot
    {
        public virtual EmailTemplate EmailTemplate { get; protected set; }

        private readonly IList<EmailPart> _parts = new List<EmailPart>();
        public virtual IEnumerable<EmailPart> Parts
        {
            get { return _parts; }
        }

        public virtual string FromAddress { get; protected set; }
        public virtual string Subject { get; protected set; }
        public virtual EmailState State { get; protected set; }

        private readonly Iesi.Collections.Generic.ISet<EmailRecipient> _emailRecipients = new HashedSet<EmailRecipient>();
        public virtual Iesi.Collections.Generic.ISet<EmailRecipient> EmailRecipients
        {
            get { return _emailRecipients; }
        }


        protected Email() {}

        public Email(EmailTemplate emailTemplate)
        {
            EmailTemplate = emailTemplate;
            State = EmailState.Draft;

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

        public virtual void UpdateVariables(EmailDto emailDto)
        {
            Guard.Hope(Id == emailDto.EmailId, "Invalid email id");
            emailDto.Parts.Each(part =>
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
            return _parts.First(x => x.Id == partId);
        }

        private void _SetVariableValue(int variablePartId, string value)
        {
            _GetVariablePart(variablePartId).SetValue(value);
        }

        public virtual void EnqueueEmailToBeSent(string fromAddress, HashedSet<Recipient> recipients, string subject)
        {
            Guard.Hope(State.CanSend, "cannot enqeue email in the current state: " + State.Name);
            Guard.Hope(EmailRecipients.Count == 0, "recipients must be empty");
            State = EmailState.ToBeSent;
            FromAddress = fromAddress;
            Subject = subject;

            recipients.Each(r => EmailRecipients.Add(new EmailRecipient(r)));

            DomainEvents.RaiseEvent(new EmailEnqueuedToBeSentEvent
                                        {
                                            EmailId = Id
                                        });
        }
    }
}
