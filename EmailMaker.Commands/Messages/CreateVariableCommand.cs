using Core.Commands;
using EmailMaker.Dtos.EmailTemplates;

namespace EmailMaker.Commands.Messages
{
    public class CreateVariableCommand : ICommand
    {
        public int HtmlTemplatePartId { get; set; }
        public int HtmlStartIndex { get; set; }
        public int Length { get; set; }
        public EmailTemplateDto EmailTemplate { get; set; }
    }
}
