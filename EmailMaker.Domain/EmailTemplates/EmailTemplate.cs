using System.Collections.Generic;
using System.Linq;
using DddCore;

namespace EmailMaker.Domain.EmailTemplates
{
    public class EmailTemplate : Identity<EmailTemplate>, IAggregateRoot
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
        }

        private HtmlEmailTemplatePart GetHtmlTemplatePart(int htmlTemplatePartId)
        {
            return (HtmlEmailTemplatePart)Parts.First(x => x.Id == htmlTemplatePartId);
        }

    }
}
