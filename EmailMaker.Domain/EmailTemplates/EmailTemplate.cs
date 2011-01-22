using System.Collections.Generic;
using System.Linq;
using DddCore;
using EmailMaker.Utilities;

namespace EmailMaker.Domain.EmailTemplates
{
    public class EmailTemplate : Identity<EmailTemplate>, IAggregateRootEntity
    {
        private readonly IList<EmailTemplatePart> _parts;
        
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

        public virtual void SetHtml(int htmlTemplatePartId, string html)
        {
            var htmlTemplatePart = GetHtmlTemplatePart(htmlTemplatePartId);
            htmlTemplatePart.SetHtml(html);
        }

        public virtual void CreateVariable(int htmlTemplatePartId, int htmlStartIndex, int length)
        {
            var htmlTemplatePart = GetHtmlTemplatePart(htmlTemplatePartId);
            var html = htmlTemplatePart.Html;
            var htmlBefore = html.Substring(0, htmlStartIndex);
            var variableValue = html.Substring(htmlStartIndex, length);
            var htmlAfter = html.Substring(htmlStartIndex + length);
            htmlTemplatePart.SetHtml(htmlBefore);
            _parts.Add(new VariableEmailTemplatePart(variableValue));
            _parts.Add(new HtmlEmailTemplatePart(htmlAfter));

            _RefreshPartPositions();
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

            _RefreshPartPositions();        
        }

        private void _RefreshPartPositions()
        {
            for (var i = 0; i < _parts.Count; i++)
            {
                _parts[i].SetPosition(i);
            }
        }

        private HtmlEmailTemplatePart GetHtmlTemplatePart(int htmlTemplatePartId)
        {
            return (HtmlEmailTemplatePart)Parts.First(x => x.Id == htmlTemplatePartId);
        }

    }
}
