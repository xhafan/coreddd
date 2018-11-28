#if !NET40 && !NET45
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDdd.Nhibernate.TestHelpers;
using CoreDdd.Queries;
using CoreIoC;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Queries
{
    [TestFixture]
    public class when_executing_queryover_query_in_query_executor_async : BaseIoCPersistenceTest
    {
        private IEnumerable<int> _result;
        private GetTestEntityCountTestQueryOverQuery _query;
      
        [SetUp]
        public async Task Context()
        {
            _persistTestEntity();
            _query = new GetTestEntityCountTestQueryOverQuery();

            var queryExecutor = IoC.Resolve<IQueryExecutor>();
            _result = await queryExecutor.ExecuteAsync<GetTestEntityCountTestQueryOverQuery, int>(_query);

            void _persistTestEntity()
            {
                var testEntityOne = new TestEntity();
                UnitOfWork.Save(testEntityOne);
                var testEntityTwo = new TestEntity();
                UnitOfWork.Save(testEntityTwo);
            }
        }

        [Test]
        public void two_entities_are_counted()
        {
            _result.Count().ShouldBe(1);
            _result.First().ShouldBe(2);
        }    
    }
}
#endif