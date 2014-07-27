using CoreTest;
using EmailMaker.Dtos.Users;
using EmailMaker.Queries.Messages;
using NUnit.Framework;
using Rhino.Mocks;

namespace EmailMaker.UnitTests.Controllers
{
    public abstract class BaseEmailmakerControllerTest : BaseControllerTest
    {
        [SetUp]
        public override void Context()
        {
            base.Context();

            QueryExecutor.Stub(x => x.Execute<GetUserDetailsByEmailAddressQuery, UserDto>(Arg<GetUserDetailsByEmailAddressQuery>.Is.Anything))
                .Return(new[] { new UserDto() });
        }
    }
}