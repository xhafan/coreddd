using Core.Commands;
using Core.Queries;
using NUnit.Framework;
using Rhino.Mocks;

namespace Core.Tests.Helpers.Controllers
{
    public abstract class base_controller_test
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
