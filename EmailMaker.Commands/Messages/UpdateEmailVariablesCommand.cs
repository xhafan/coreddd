using Core.Commands;
using EmailMaker.Dtos.Emails;

namespace EmailMaker.Commands.Messages
{
    public class UpdateEmailVariablesCommand : ICommand
    {
        public EmailDto Email { get; set; }
    }
}