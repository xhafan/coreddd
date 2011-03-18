using Core.Commands;
using Core.Domain;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.EmailTemplates;

namespace EmailMaker.Commands.Handlers
{
    public class CreateEmailCommandHandler : BaseCommandHandler<CreateEmailCommand>
    {
        private readonly IRepository<Email> _emailRepository;
        private readonly IRepository<EmailTemplate> _emailTemplateRepository;

        public CreateEmailCommandHandler(IRepository<Email> emailRepository, IRepository<EmailTemplate> emailTemplateRepository)
        {
            _emailTemplateRepository = emailTemplateRepository;
            _emailRepository = emailRepository;
        }

        public override void Execute(CreateEmailCommand command)
        {
            var emailTemplate = _emailTemplateRepository.GetById(command.EmailTemplateId);
            var newEmail = new Email(emailTemplate);
            _emailRepository.Save(newEmail);
            RaiseEvent(new CommandExecutedArgs { Args = newEmail.Id });
        }
    }
}