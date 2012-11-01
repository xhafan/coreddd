using CoreDdd.Queries;
using EmailMaker.Dtos.Users;
using EmailMaker.Queries.Messages;
using NHibernate;

namespace EmailMaker.Queries.Handlers
{
    public class GetUserDetailsByEmailAddressQueryHandler : BaseNhibernateCriteriaQueryHandler<GetUserDetailsByEmailAddressQuery>
    {        
        public override ICriteria GetCriteria<TResult>(GetUserDetailsByEmailAddressQuery query)
        {           
            return Session.QueryOver<UserDto>()
                .Where(x => x.EmailAddress == query.EmailAddress)
                .UnderlyingCriteria;     
        }
    }
}
