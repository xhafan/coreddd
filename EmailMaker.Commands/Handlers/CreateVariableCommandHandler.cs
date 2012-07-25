using Core.Commands;
using Core.Domain;
using Core.Domain.Repositories;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.EmailTemplates;

namespace EmailMaker.Commands.Handlers
{
    public class CreateVariableCommandHandler : BaseCommandHandler<CreateVariableCommand>
    {
        private readonly IRepository<EmailTemplate> _emailTemplateRepository;

        public CreateVariableCommandHandler(IRepository<EmailTemplate> emailTemplateRepository)
        {
            _emailTemplateRepository = emailTemplateRepository;
        }

        public override void Execute(CreateVariableCommand command)
        {
            var emailTemplate = _emailTemplateRepository.GetById(command.EmailTemplate.EmailTemplateId);
            emailTemplate.Update(command.EmailTemplate);
            emailTemplate.CreateVariable(command.HtmlTemplatePartId, command.HtmlStartIndex, command.Length);
        }
    }
}
