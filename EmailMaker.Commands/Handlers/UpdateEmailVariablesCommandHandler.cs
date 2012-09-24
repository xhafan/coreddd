using CoreDdd.Commands;
using CoreDdd.Domain;
using CoreDdd.Domain.Repositories;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.Emails;

namespace EmailMaker.Commands.Handlers
{
    public class UpdateEmailVariablesCommandHandler : BaseCommandHandler<UpdateEmailVariablesCommand>
    {
        private readonly IRepository<Email> _emailRepository;

        public UpdateEmailVariablesCommandHandler(IRepository<Email> emailRepository)
        {
            _emailRepository = emailRepository;
        }

        public override void Execute(UpdateEmailVariablesCommand command)
        {
            var email = _emailRepository.GetById(command.Email.EmailId);
            email.UpdateVariables(command.Email);
        }
    }
}