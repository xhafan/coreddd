using Core.Commands;

namespace EmailMaker.Commands.Messages
{
    public class CreateUserCommand : ICommand
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}