using Core.Commands;
using EmailMaker.DTO.EmailTemplate;

namespace EmailMaker.Commands.Messages
{
    public class DeleteVariableCommand : ICommandMessage
    {
        public int VariablePartId { get; set; }
        public EmailTemplateDTO EmailTemplate { get; set; }
    }
}