using System;
using System.Linq;
using Core.Commands;
using Core.Domain;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.Emails;

namespace EmailMaker.Commands.Handlers
{
    public class SaveEmailRecipientsCommandHandler : BaseCommandHandler<SaveEmailRecipientsCommand>
    {
        private readonly IRepository<Email> _emailRepository;

        public SaveEmailRecipientsCommandHandler(IRepository<Email> emailRepository)
        {
            _emailRepository = emailRepository;
        }

        public override void Execute(SaveEmailRecipientsCommand command)
        {
            var email = _emailRepository.GetById(command.EmailId);
            var toAddresses = command.ToAddressesStr.Split(new[]{","}, StringSplitOptions.RemoveEmptyEntries).Select(addr => addr.Trim());
            email.SetFromAddressAndRecipients(command.FromAddress, toAddresses);
        }
    }
}