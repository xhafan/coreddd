using Core.TestHelper.Controllers;
using EmailMaker.DTO.Users;
using EmailMaker.Queries.Messages;
using Rhino.Mocks;

namespace EmailMaker.Controllers.Tests
{
    public abstract class BaseEmailMakerControllerTest : BaseControllerTest
    {
        public override void ExtraSetUp()
        {
            QueryExecutor.Stub(a => a.Execute<GetUserDetailsByEmailAddressMessage, UserDTO>(Arg<GetUserDetailsByEmailAddressMessage>.Is.Anything))
                .Return(new[] { new UserDTO() });
        }
    }
}