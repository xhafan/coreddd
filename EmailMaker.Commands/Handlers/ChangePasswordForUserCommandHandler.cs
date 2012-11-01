using CoreDdd.Commands;
using CoreDdd.Domain.Repositories;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.Users;

namespace EmailMaker.Commands.Handlers
{
    public class ChangePasswordForUserCommandHandler : BaseCommandHandler<ChangePasswordForUserCommand>
    {
        private readonly IRepository<User> _userRepository;

        public ChangePasswordForUserCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public override void Execute(ChangePasswordForUserCommand command)
        {
            var user = _userRepository.GetById(command.UserId);
            user.ChangePassword(command.OldPassword, command.NewPassword);
        }
    }
}