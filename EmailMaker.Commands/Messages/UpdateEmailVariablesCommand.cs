using Core.Commands;
using EmailMaker.DTO.Emails;

namespace EmailMaker.Commands.Messages
{
    public class UpdateEmailVariablesCommand : ICommand
    {
        public EmailDTO Email { get; set; }
    }
}