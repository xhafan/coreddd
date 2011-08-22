using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.Domain;
using Core.Utilities;
using Core.Utilities.Extensions;
using EmailMaker.DTO;
using EmailMaker.DTO.EmailTemplates;
using EmailMaker.Utilities;

namespace EmailMaker.Domain.EmailTemplates
{
    public class EmailTemplate : Identity<EmailTemplate>, IAggregateRootEntity
    {
        private readonly IList<EmailTemplatePart> _parts;

        public virtual string Name { get; set; }
        
        public virtual IEnumerable<EmailTemplatePart> Parts
        {
            get { return _parts; }
        }

        public EmailTemplate()
        {
            _parts = new List<EmailTemplatePart> { new HtmlEmailTemplatePart() };
        }

        public EmailTemplate(string html)
        {
            _parts = new List<EmailTemplatePart> { new HtmlEmailTemplatePart(html) };
        }

        public EmailTemplate(string html, string name) : this(html)
        {
            Name = name;
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

        public virtual void Update(EmailTemplateDTO emailTemplateDTO)
        {
            Guard.Hope(Id == emailTemplateDTO.EmailTemplateId, "Invalid email template id");
            emailTemplateDTO.Parts.Each(part =>
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
            Name = emailTemplateDTO.Name;
        }
    }
}
