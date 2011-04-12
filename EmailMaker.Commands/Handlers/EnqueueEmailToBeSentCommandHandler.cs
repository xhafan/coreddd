using System;
using System.Linq;
using Core.Commands;
using Core.Domain;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.Emails;

namespace EmailMaker.Commands.Handlers
{
    public class EnqueueEmailToBeSentCommandHandler : BaseCommandHandler<EnqueueEmailToBeSentCommand>
    {
        private readonly IRepository<Email> _emailRepository;

        public EnqueueEmailToBeSentCommandHandler(IRepository<Email> emailRepository)
        {
            _emailRepository = emailRepository;
        }

        public override void Execute(EnqueueEmailToBeSentCommand command)
        {
            var email = _emailRepository.GetById(command.EmailId);
            var toAddresses = command.ToAddressesStr.Split(new[]{","}, StringSplitOptions.RemoveEmptyEntries).Select(addr => addr.Trim());
            email.EnqueueEmailToBeSent(command.FromAddress, toAddresses, command.Subject);
        }
    }
}