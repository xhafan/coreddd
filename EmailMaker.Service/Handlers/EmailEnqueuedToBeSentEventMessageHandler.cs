using System;
using Core.Queries;
using EmailMaker.Messages;
using NServiceBus;

namespace EmailMaker.Service.Handlers
{
    public class EmailEnqueuedToBeSentEventMessageHandler : IMessageHandler<EmailEnqueuedToBeSentEventMessage>
    {
        private readonly IQueryExecutor _queryExecutor;

        public EmailEnqueuedToBeSentEventMessageHandler(IQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor;
        }

        public void Handle(EmailEnqueuedToBeSentEventMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
