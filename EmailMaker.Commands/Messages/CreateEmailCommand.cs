using Core.Commands;

namespace EmailMaker.Commands.Messages
{
    public class CreateEmailCommand : ICommand
    {
        public int EmailTemplateId { get; set; }
    }
}