using Core.Commands;
using Core.Domain;
using Core.Domain.Repositories;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.EmailTemplates;

namespace EmailMaker.Commands.Handlers
{
    public class SaveEmailTemplateCommandHandler : BaseCommandHandler<SaveEmailTemplateCommand>
    {
        private readonly IRepository<EmailTemplate> _emailTemplateRepository;

        public SaveEmailTemplateCommandHandler(IRepository<EmailTemplate> emailTemplateRepository)
        {
            _emailTemplateRepository = emailTemplateRepository;
        }

        public override void Execute(SaveEmailTemplateCommand command)
        {
            var emailTemplate = _emailTemplateRepository.GetById(command.EmailTemplate.EmailTemplateId);
            emailTemplate.Update(command.EmailTemplate);
        }
    }
}