using CoreTest;
using EmailMaker.Dtos.Users;
using EmailMaker.Queries.Messages;
using FakeItEasy;
using NUnit.Framework;

namespace EmailMaker.UnitTests.Controllers
{
    public abstract class BaseEmailmakerControllerTest : BaseControllerTest
    {
        [SetUp]
        public override void Context()
        {
            base.Context();

            A.CallTo(() => QueryExecutor.Execute<GetUserDetailsByEmailAddressQuery, UserDto>(A<GetUserDetailsByEmailAddressQuery>.That.IsNotNull()))
                .Returns(new[] { new UserDto() });
        }
    }
}