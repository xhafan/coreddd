using Core.Commands;
using EmailMaker.DTO.EmailTemplate;

namespace EmailMaker.Commands.Messages
{
    public class SaveTemplateCommand : ICommand
    {
        public EmailTemplateDTO EmailTemplate { get; set; }
    }
}