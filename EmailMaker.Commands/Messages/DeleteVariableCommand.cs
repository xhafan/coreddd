using Core.Commands;
using EmailMaker.Web.DTO.EmailTemplate;

namespace EmailMaker.Commands.Messages
{
    public class DeleteVariableCommand : ICommandMessage
    {
        public int VariablePartId { get; set; }
        public EmailTemplateDTO EmailTemplate { get; set; }
    }
}