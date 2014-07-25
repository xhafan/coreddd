using CoreDdd.Commands;
using CoreDdd.Queries;
using CoreTest;
using NUnit.Framework;

namespace CoreDdd.TestHelpers.Controllers
{
    public abstract class BaseControllerTest : BaseTest
    {
        protected ICommandExecutor CommandExecutor;
        protected IQueryExecutor QueryExecutor;

        public abstract void ExtraSetUp();
        public abstract void Context();

        [SetUp]
        public void SetUp()
        {
            CommandExecutor = Mock<ICommandExecutor>();
            QueryExecutor = Mock<IQueryExecutor>();
            ExtraSetUp();
            Context();
        }
    }
}
