using System.Linq;
using System.Web.Mvc;
using CoreDdd.Queries;
using EmailMaker.Dtos.Users;
using EmailMaker.Queries.Messages;

namespace EmailMaker.Controllers.BaseController
{
    [Authorize]
    public class AuthenticatedController : Controller
    {
        private readonly IQueryExecutor _queryExecutor;

        public AuthenticatedController(IQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor;
        }

        public int UserId 
        { 
            get
            {
                // todo: fix this user id retrieval via cookie persistence or in identity
                var message = new GetUserDetailsByEmailAddressMessage {EmailAddress = User.Identity.Name};
                var user = _queryExecutor.Execute<GetUserDetailsByEmailAddressMessage, UserDto>(message).First();
                return user.UserId;
            }
        }
    }
}
