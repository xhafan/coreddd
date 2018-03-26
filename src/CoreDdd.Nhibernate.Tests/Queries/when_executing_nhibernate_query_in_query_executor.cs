using System.Collections.Generic;
using System.Linq;
using CoreDdd.Nhibernate.TestHelpers;
using CoreDdd.Nhibernate.Tests.TestEntities;
using CoreDdd.Queries;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Queries
{
    [TestFixture]
    public class when_executing_nhibernate_query_in_query_executor : BasePersistenceTest
    {
        private IEnumerable<int> _result;
        private GetTestEntityCountTestNhibernateQuery _query;
      
        [SetUp]
        public void Context()
        {
            _persistTestEntity();
            _query = new GetTestEntityCountTestNhibernateQuery();

            var queryExecutor = new QueryExecutor();
            _result = queryExecutor.Execute<GetTestEntityCountTestNhibernateQuery, int>(_query);

            void _persistTestEntity()
            {
                var testEntityOne = new TestEntity();
                Save(testEntityOne);
                var testEntityTwo = new TestEntity();
                Save(testEntityTwo);
            }
        }

        [Test]
        public void result_is_correct()
        {
            _result.Count().ShouldBe(1);
            _result.First().ShouldBe(2);
        }
    
    }
}