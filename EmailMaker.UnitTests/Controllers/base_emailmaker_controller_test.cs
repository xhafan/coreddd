using Core.Tests.Helpers.Controllers;
using EmailMaker.Dtos.Users;
using EmailMaker.Queries.Messages;
using Rhino.Mocks;

namespace EmailMaker.UnitTests.Controllers
{
    public abstract class base_emailmaker_controller_test : base_controller_test
    {
        public override void ExtraSetUp()
        {
            QueryExecutor.Stub(a => a.Execute<GetUserDetailsByEmailAddressMessage, UserDto>(Arg<GetUserDetailsByEmailAddressMessage>.Is.Anything))
                .Return(new[] { new UserDto() });
        }
    }
}