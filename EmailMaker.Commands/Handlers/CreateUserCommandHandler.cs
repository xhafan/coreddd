using CoreDdd.Commands;
using CoreDdd.Domain;
using CoreDdd.Domain.Repositories;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.Users;

namespace EmailMaker.Commands.Handlers
{
    public class CreateUserCommandHandler : BaseCommandHandler<CreateUserCommand>
    {
        private readonly IRepository<User> _userRepository;

        public CreateUserCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public override void Execute(CreateUserCommand command)
        {
            var newUser = new User("", "", command.EmailAddress, command.Password);
            _userRepository.Save(newUser);
        }
    }
}