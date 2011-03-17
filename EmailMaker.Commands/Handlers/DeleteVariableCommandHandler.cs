using Core.Commands;
using Core.Domain;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.EmailTemplates;

namespace EmailMaker.Commands.Handlers
{
    public class DeleteVariableCommandHandler : BaseCommandHandler<DeleteVariableCommand>
    {
        private readonly IRepository<EmailTemplate> _emailTemplateRepository;

        public DeleteVariableCommandHandler(IRepository<EmailTemplate> emailTemplateRepository)
        {
            _emailTemplateRepository = emailTemplateRepository;
        }

        public override void Execute(DeleteVariableCommand command)
        {
            var emailTemplate = _emailTemplateRepository.GetById(command.EmailTemplate.EmailTemplateId);
            emailTemplate.Update(command.EmailTemplate);
            emailTemplate.DeleteVariable(command.VariablePartId);
        }
    }
}