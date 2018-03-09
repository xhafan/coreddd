using CoreDdd.Commands;
using CoreDdd.Queries;
using FakeItEasy;
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
            CommandExecutor = A.Fake<ICommandExecutor>();
            QueryExecutor = A.Fake<IQueryExecutor>();
        }
    }
}
