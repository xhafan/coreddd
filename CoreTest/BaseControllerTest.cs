using CoreDdd.Commands;
using CoreDdd.Queries;
using NUnit.Framework;

namespace CoreTest
{
    // todo: move this into some CoreControllerTest assembly
    public abstract class BaseControllerTest : BaseTest
    {
        protected ICommandExecutor CommandExecutor;
        protected IQueryExecutor QueryExecutor;        

        [SetUp]
        public virtual void Context()
        {
            CommandExecutor = Mock<ICommandExecutor>();
            QueryExecutor = Mock<IQueryExecutor>();
        }
    }
}
