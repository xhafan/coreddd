using Core.Commands;
using EmailMaker.Dtos.EmailTemplates;

namespace EmailMaker.Commands.Messages
{
    public class SaveEmailTemplateCommand : ICommand
    {
        public EmailTemplateDto EmailTemplate { get; set; }
    }
}