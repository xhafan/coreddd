using Core.Commands;
using EmailMaker.DTO.EmailTemplate;

namespace EmailMaker.Commands.Messages
{
    public class SaveEmailTemplateCommand : ICommand
    {
        public EmailTemplateDTO EmailTemplate { get; set; }
    }
}