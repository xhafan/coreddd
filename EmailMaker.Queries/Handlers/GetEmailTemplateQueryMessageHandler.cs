using System;
using System.Collections.Generic;
using Core.Queries;
using EmailMaker.Queries.Messages;

namespace EmailMaker.Queries.Handlers
{
    public class GetEmailTemplateQueryMessageHandler : IQueryMessageHandler<GetEmailTemplateQueryMessage>
    {
        public IEnumerable<TResult> Execute<TResult>(GetEmailTemplateQueryMessage queryMessage)
        {
            throw new NotImplementedException();
        }
    }
}