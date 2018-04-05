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
    public class when_executing_ado_net_query_in_query_executor : BasePersistenceTest
    {
        private IEnumerable<int> _result;
        private GetTestEntityCountTestAdoNetQuery _query;
      
        [SetUp]
        public void Context()
        {
            _persistTestEntity();
            _query = new GetTestEntityCountTestAdoNetQuery();

            var queryExecutor = new QueryExecutor();
            _result = queryExecutor.Execute<GetTestEntityCountTestAdoNetQuery, int>(_query);

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