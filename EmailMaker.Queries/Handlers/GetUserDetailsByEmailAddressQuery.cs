using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Queries;
using EmailMaker.DTO.Users;
using EmailMaker.Domain.Users;
using EmailMaker.Queries.Messages;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace EmailMaker.Queries.Handlers
{
    public class GetUserDetailsByEmailAddressQuery : BaseNHibernateCriteriaQueryMessageHandler<GetUserDetailsByEmailAddressMessage>
    {
        
        public override ICriteria GetCriteria<TResult>(GetUserDetailsByEmailAddressMessage message)
        {
           
            UserDTO userDto = null;

            return Session.QueryOver<User>()
               .Where(user => user.EmailAddress == message.EmailAddress)
               .SelectList(list => list
                                .Select(c => c.Id).WithAlias(() => userDto.UserId)
                                .Select(c => c.FirstName).WithAlias(() => userDto.FirstName)
                                .Select(c => c.LastName).WithAlias(() => userDto.LastName)
                                .Select(c => c.EmailAddress).WithAlias(() => userDto.EmailAddress)
                                .Select(c => c.Password).WithAlias(() => userDto.Password))
              .TransformUsing(Transformers.AliasToBean<UserDTO>())
              .UnderlyingCriteria;
     
        }
    }
}
