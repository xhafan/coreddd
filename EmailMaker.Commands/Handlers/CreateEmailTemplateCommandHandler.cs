using Core.Commands;
using Core.Domain;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.EmailTemplates;

namespace EmailMaker.Commands.Handlers
{
    public class CreateEmailTemplateCommandHandler : BaseCommandHandler<CreateEmailTemplateCommand>
    {
        private readonly IRepository<EmailTemplate> _emailTemplateRepository;

        public CreateEmailTemplateCommandHandler(IRepository<EmailTemplate> emailTemplateRepository)
        {
            _emailTemplateRepository = emailTemplateRepository;
        }

        public override void Execute(CreateEmailTemplateCommand command)
        {
            var newEmailTemplate = new EmailTemplate(command.UserId);
            _emailTemplateRepository.Save(newEmailTemplate);
            RaiseEvent(new CommandExecutedArgs { Args = newEmailTemplate.Id });
        }
    }
}