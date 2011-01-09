using System.Collections.Generic;
using System.Linq;

namespace EmailMaker.Domain.EmailTemplates
{
    public abstract class TemplatePartContainer : TemplatePart
    {
        public IList<TemplatePart> Parts { get; private set; }

        protected TemplatePartContainer()
        {
            Parts = new List<TemplatePart>();
        }

        public void SetHtml(int htmlTemplatePartId, string html)
        {
            var htmlTemplatePart = GetHtmlTemplatePart(htmlTemplatePartId);
            htmlTemplatePart.SetHtml(html);
        }

        public void CreateVariable(int htmlTemplatePartId, int htmlStartIndex, int length)
        {
            var htmlTemplatePart = GetHtmlTemplatePart(htmlTemplatePartId);
            var html = htmlTemplatePart.Html;
            var htmlBefore = html.Substring(0, htmlStartIndex);
            var variableValue = html.Substring(htmlStartIndex, length);
            var htmlAfter = html.Substring(htmlStartIndex + length);
            htmlTemplatePart.SetHtml(htmlBefore);
            Parts.Add(new VariableTemplatePart(variableValue));
            Parts.Add(new HtmlTemplatePart(htmlAfter));
        }

        private HtmlTemplatePart GetHtmlTemplatePart(int htmlTemplatePartId)
        {
            return (HtmlTemplatePart)Parts.First(x => x.Id == htmlTemplatePartId);
        }
    }
}