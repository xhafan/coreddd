using Core.Commands;
using Core.Queries;
using EmailMaker.DTO.Users;
using EmailMaker.Queries.Messages;
using NUnit.Framework;
using Rhino.Mocks;

namespace Core.TestHelper.Controllers
{
    public abstract class BaseControllerTest
    {
        protected ICommandExecutor CommandExecutor;
        protected IQueryExecutor QueryExecutor;

        public abstract void Context();

        [SetUp]
        public void SetUp()
        {
            CommandExecutor = MockRepository.GenerateMock<ICommandExecutor>();
            QueryExecutor = MockRepository.GenerateMock<IQueryExecutor>();
            QueryExecutor.Stub(a => a.Execute<GetUserDetailsByEmailAddressMessage, UserDTO>(Arg<GetUserDetailsByEmailAddressMessage>.Is.Anything))
                .Return(new[] { new UserDTO() });

            Context();
        }
    }
}
