using Core.Commands;

namespace EmailMaker.Commands.Messages
{
    public class CreateEmailTemplateCommand : ICommand
    {
        public int UserId { get; set; }
    }
}