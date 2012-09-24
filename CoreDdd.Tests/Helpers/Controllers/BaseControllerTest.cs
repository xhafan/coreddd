using CoreDdd.Commands;
using CoreDdd.Queries;
using NUnit.Framework;
using Rhino.Mocks;

namespace CoreDdd.Tests.Helpers.Controllers
{
    public abstract class BaseControllerTest
    {
        protected ICommandExecutor CommandExecutor;
        protected IQueryExecutor QueryExecutor;

        public abstract void ExtraSetUp();
        public abstract void Context();

        [SetUp]
        public void SetUp()
        {
            CommandExecutor = MockRepository.GenerateMock<ICommandExecutor>();
            QueryExecutor = MockRepository.GenerateMock<IQueryExecutor>();
            ExtraSetUp();
            Context();
        }
    }
}
