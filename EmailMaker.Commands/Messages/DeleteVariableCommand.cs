using CoreDdd.Commands;
using EmailMaker.Dtos.EmailTemplates;

namespace EmailMaker.Commands.Messages
{
    public class DeleteVariableCommand : ICommand
    {
        public int VariablePartId { get; set; }
        public EmailTemplateDto EmailTemplate { get; set; }
    }
}