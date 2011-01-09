using System.Linq;
using EmailMaker.Domain.EmailTemplates;

namespace TestHelper.Builders.EmailTemplates
{
    public class EmailTemplateBuilder
    {
        private string _initialHtml;

        public static EmailTemplateBuilder New
        {
            get
            {
                return new EmailTemplateBuilder();
            }
        }

        public EmailTemplateBuilder WithInitialHtml(string html)
        {
            _initialHtml = html;
            return this;
        }

        public EmailTemplate Build()
        {
            var emailTemplate = new EmailTemplate();
            var htmlTeplatePartId = emailTemplate.Parts.First().Id;
            emailTemplate.SetHtml(htmlTeplatePartId, _initialHtml);
            return emailTemplate;
        }
    }
}
