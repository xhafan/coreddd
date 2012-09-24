using System.Collections.Generic;
using System.Linq;
using CoreDdd.Domain;
using CoreDdd.Utilities;
using CoreDdd.Utilities.Extensions;
using EmailMaker.Core;
using EmailMaker.Dtos;
using EmailMaker.Dtos.EmailTemplates;

namespace EmailMaker.Domain.EmailTemplates
{
    public class EmailTemplate : Entity<EmailTemplate>, IAggregateRoot
    {
        public virtual string Name { get; protected set; }
        private readonly IList<EmailTemplatePart> _parts = new List<EmailTemplatePart>();
        public virtual IEnumerable<EmailTemplatePart> Parts
        {
            get { return _parts; }
        }
        public virtual int UserId { get; protected set; }

        protected EmailTemplate() {}

        public EmailTemplate(int userId)
        {
            Name = null;
            _parts.Add(new HtmlEmailTemplatePart());
            UserId = userId;        
        }

        private void _SetHtml(int htmlTemplatePartId, string html)
        {
            var htmlTemplatePart = _GetHtmlPart(htmlTemplatePartId);
            htmlTemplatePart.SetHtml(html);
        }

        public virtual void CreateVariable(int htmlTemplatePartId, int htmlStartIndex, int length)
        {
            var htmlTemplatePart = _GetHtmlPart(htmlTemplatePartId);
            var html = htmlTemplatePart.Html;
            var htmlBefore = html.Substring(0, htmlStartIndex);
            var variableValue = html.Substring(htmlStartIndex, length);
            var htmlAfter = html.Substring(htmlStartIndex + length);
            htmlTemplatePart.SetHtml(htmlBefore);
            var indexOfHtmlPart = _parts.IndexOf(htmlTemplatePart);
            _parts.Insert(indexOfHtmlPart + 1, new VariableEmailTemplatePart(variableValue));
            _parts.Insert(indexOfHtmlPart + 2, new HtmlEmailTemplatePart(htmlAfter));
        }

        public virtual void DeleteVariable(int variableTemplatePartId)
        {
            var i = 1;
            while (i < _parts.Count && _parts[i].Id != variableTemplatePartId)
            {
                i++;
            }
            Guard.Hope(i != _parts.Count, "Invalid variable part Id: " + variableTemplatePartId);
            Guard.Hope(_parts[i] is VariableEmailTemplatePart, "Part is not a variable, Id: " + variableTemplatePartId);
            var htmlBeforePart = (HtmlEmailTemplatePart)_parts[i - 1];
            var variablePart = (VariableEmailTemplatePart)_parts[i];
            var htmlAfterPart = (HtmlEmailTemplatePart)_parts[i + 1];
            htmlBeforePart.SetHtml(string.Concat(htmlBeforePart.Html, variablePart.Value, htmlAfterPart.Html));

            _parts.RemoveAt(i + 1);
            _parts.RemoveAt(i);
        }

        private HtmlEmailTemplatePart _GetHtmlPart(int htmlTemplatePartId)
        {
            return (HtmlEmailTemplatePart) _GetPart(htmlTemplatePartId);
        }

        private VariableEmailTemplatePart _GetVariablePart(int variablePartId)
        {
            return (VariableEmailTemplatePart) _GetPart(variablePartId);
        }

        private EmailTemplatePart _GetPart(int partId)
        {
            return Parts.First(x => x.Id == partId);
        }

        private void _SetVariableValue(int variablePartId, string value)
        {
            _GetVariablePart(variablePartId).SetValue(value);
        }

        public virtual void Update(EmailTemplateDto emailTemplateDto)
        {
            Guard.Hope(Id == emailTemplateDto.EmailTemplateId, "Invalid email template id");
            emailTemplateDto.Parts.Each(part =>
            {
                if (part.PartType == PartType.Html)
                {
                    _SetHtml(part.PartId, part.Html);
                }
                else if (part.PartType == PartType.Variable)
                {
                    _SetVariableValue(part.PartId, part.VariableValue);
                }
                else
                {
                    throw new EmailMakerException("Unknown email template part type: " + part.PartType);
                }
            });
            Name = emailTemplateDto.Name;
        }
    }
}
