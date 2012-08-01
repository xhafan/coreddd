using Core.Queries;
using EmailMaker.Dtos.Users;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetUserDetailsByEmailAddressQuery : BaseNHibernateCriteriaQueryMessageHandler<GetUserDetailsByEmailAddressMessage>
    {        
        public override ICriteria GetCriteria<TResult>(GetUserDetailsByEmailAddressMessage message)
        {           
            return Session.QueryOver<UserDto>()
                .Where(x => x.EmailAddress == message.EmailAddress)
                .UnderlyingCriteria;     
        }
    }
}
