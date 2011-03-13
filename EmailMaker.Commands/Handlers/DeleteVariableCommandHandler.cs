using Core.Commands;
using Core.Domain;
using Core.Utilities.Extensions;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.DTO.EmailTemplate;
using EmailMaker.Utilities;

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
            command.EmailTemplate.Parts.Each(part =>
                                                 {
                                                     if (part.EmailTemplatePartType == EmailTemplatePartType.Html)
                                                     {
                                                         emailTemplate.SetHtml(part.PartId, part.Html);
                                                     }
                                                     else if (part.EmailTemplatePartType == EmailTemplatePartType.Variable)
                                                     {
                                                         emailTemplate.SetVariableValue(part.PartId, part.VariableValue);
                                                     }
                                                     else
                                                     {
                                                         throw new EmailMakerException("Unknown email template part, email template id: " + command.EmailTemplate.EmailTemplateId);
                                                     }
                                                 });
            emailTemplate.DeleteVariable(command.VariablePartId);
        }
    }
}