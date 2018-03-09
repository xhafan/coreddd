using CoreDdd.Commands;

namespace EmailMaker.Commands.Messages
{
    public class ChangePasswordForUserCommand : ICommand
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
