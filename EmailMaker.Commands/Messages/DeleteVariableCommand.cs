using Core.Commands;
using EmailMaker.DTO.EmailTemplates;

namespace EmailMaker.Commands.Messages
{
    public class DeleteVariableCommand : ICommand
    {
        public int VariablePartId { get; set; }
        public EmailTemplateDTO EmailTemplate { get; set; }
    }
}