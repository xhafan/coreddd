#if !NET40 && !NET45
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDdd.Nhibernate.TestHelpers;
using CoreDdd.Nhibernate.Tests.TestEntities;
using CoreDdd.Queries;
using CoreIoC;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Queries
{
    [TestFixture]
    public class when_executing_nhibernate_query_in_query_executor_async : BasePersistenceTest
    {
        private IEnumerable<int> _result;
        private GetTestEntityCountTestNhibernateQuery _query;
      
        [SetUp]
        public async Task Context()
        {
            _persistTestEntity();
            _query = new GetTestEntityCountTestNhibernateQuery();

            var queryExecutor = IoC.Resolve<IQueryExecutor>();
            _result = await queryExecutor.ExecuteAsync<GetTestEntityCountTestNhibernateQuery, int>(_query);

            void _persistTestEntity()
            {
                var testEntityOne = new TestEntity();
                Save(testEntityOne);
                var testEntityTwo = new TestEntity();
                Save(testEntityTwo);
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