using System.Linq;
using CoreDdd.Commands;
using CoreDdd.Domain.Repositories;
using CoreDdd.Queries;
using CoreDdd.Utilities.Extensions;
using EmailMaker.Commands.Messages;
using EmailMaker.Domain.Emails;
using EmailMaker.Queries.Messages;
using Iesi.Collections.Generic;

namespace EmailMaker.Commands.Handlers
{
    public class EnqueueEmailToBeSentCommandHandler : BaseCommandHandler<EnqueueEmailToBeSentCommand>
    {
        private readonly IRepository<Email> _emailRepository;
        private readonly IRecipientParser _recipientParser;
        private readonly IQueryExecutor _queryExecutor;
        private readonly IRepository<Recipient> _recipientRepository;

        public EnqueueEmailToBeSentCommandHandler(IRepository<Email> emailRepository, IRecipientParser recipientParser, IQueryExecutor queryExecutor,
            IRepository<Recipient> recipientRepository)
        {
            _recipientRepository = recipientRepository;
            _emailRepository = emailRepository;
            _queryExecutor = queryExecutor;
            _recipientParser = recipientParser;
        }

        public override void Execute(EnqueueEmailToBeSentCommand command)
        {
            var email = _emailRepository.GetById(command.EmailId);
            var emailAddressesAndNames = _recipientParser.Parse(command.Recipients);
            var existingRecipients = _queryExecutor.Execute<GetExistingRecipientsQueryMessage, Recipient>(
                new GetExistingRecipientsQueryMessage
                    {
                        RecipientEmailAddresses = emailAddressesAndNames.Keys
                    }).ToDictionary(k => k.EmailAddress);
            var recipients = new HashedSet<Recipient>(existingRecipients.Values);
            var recipientsToBeCreated = emailAddressesAndNames.Where(p => !existingRecipients.ContainsKey(p.Key));
            recipientsToBeCreated.Each(r =>
                                           {
                                               var newRecipient = new Recipient(r.Key, r.Value);
                                               _recipientRepository.Save(newRecipient);
                                               recipients.Add(newRecipient);
                                           });
            email.EnqueueEmailToBeSent(command.FromAddress, recipients, command.Subject);
        }
    }
}