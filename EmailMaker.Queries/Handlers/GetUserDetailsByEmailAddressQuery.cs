using Core.Queries;
using EmailMaker.Domain.Users;
using EmailMaker.Dtos.Users;
using EmailMaker.Queries.Messages;
using NHibernate;
using NHibernate.Transform;

namespace EmailMaker.Queries.Handlers
{
    // todo: refactor this via UserDto DB view
    public class GetUserDetailsByEmailAddressQuery : BaseNHibernateCriteriaQueryMessageHandler<GetUserDetailsByEmailAddressMessage>
    {        
        public override ICriteria GetCriteria<TResult>(GetUserDetailsByEmailAddressMessage message)
        {           
            UserDto userDto = null;
            return Session.QueryOver<User>()
               .Where(user => user.EmailAddress == message.EmailAddress)
               .SelectList(list => list
                                .Select(c => c.Id).WithAlias(() => userDto.UserId)
                                .Select(c => c.FirstName).WithAlias(() => userDto.FirstName)
                                .Select(c => c.LastName).WithAlias(() => userDto.LastName)
                                .Select(c => c.EmailAddress).WithAlias(() => userDto.EmailAddress)
                                .Select(c => c.Password).WithAlias(() => userDto.Password))
              .TransformUsing(Transformers.AliasToBean<UserDto>())
              .UnderlyingCriteria;     
        }
    }
}
