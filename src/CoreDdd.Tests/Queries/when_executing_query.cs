using System.Collections.Generic;
using System.Linq;
using CoreDdd.Queries;
using CoreIoC;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Tests.Queries
{
    [TestFixture]
    public class when_executing_query
    {
        private IEnumerable<int> _result;
        private IQueryHandler<TestQuery> _testQueryHandler;
        private TestQuery _testQuery;

        public class TestQuery : IQuery {}
        
        [SetUp]
        public void Context()
        {
            var container = A.Fake<IContainer>();
            IoC.Initialize(container);

            _testQueryHandler = A.Fake<IQueryHandler<TestQuery>>();
            _testQuery = new TestQuery();
            A.CallTo(() => _testQueryHandler.Execute<int>(A<TestQuery>.That.Matches(p => p == _testQuery))).Returns(new[] { 23 });

            A.CallTo(() => container.Resolve<IQueryHandler<TestQuery>>()).Returns(_testQueryHandler);

            var queryExecutor = new QueryExecutor();
            _result = queryExecutor.Execute<TestQuery, int>(_testQuery);
        }

        [Test]
        public void query_was_executed_by_handler()
        {
            A.CallTo(() => _testQueryHandler.Execute<int>(_testQuery)).MustHaveHappened();
        }

        [Test]
        public void result_is_correct()
        {
            _result.Count().ShouldBe(1);
            _result.First().ShouldBe(23);
        }
    
    }
}
