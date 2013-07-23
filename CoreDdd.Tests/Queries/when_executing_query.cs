using System.Collections.Generic;
using System.Linq;
using Castle.Windsor;
using CoreDdd.Queries;
using CoreIoC;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace CoreDdd.Tests.Queries
{
    [TestFixture]
    public class when_executing_query
    {
        private IEnumerable<bool> _result;
        private IQueryHandler<TestQuery> _testQueryHandler;
        private TestQuery _testQuery;

        public class TestQuery : IQuery {}
        
        [SetUp]
        public void Context()
        {
            var container = MockRepository.GenerateStub<IWindsorContainer>();
            IoC.Initialize(container);

            _testQueryHandler = MockRepository.GenerateMock<IQueryHandler<TestQuery>>();
            _testQuery = new TestQuery();
            _testQueryHandler.Expect(a => a.Execute<bool>(Arg<TestQuery>.Matches(p => p == _testQuery))).Return(new[] { true });

            container.Stub(a => a.Resolve<IQueryHandler<TestQuery>>()).Return(_testQueryHandler);

            var queryExecutor = new QueryExecutor();
            _result = queryExecutor.Execute<TestQuery, bool>(_testQuery);
        }

        [Test]
        public void query_was_executed_by_handler()
        {
            _testQueryHandler.AssertWasCalled(a => a.Execute<bool>(_testQuery));            
        }

        [Test]
        public void result_is_correct()
        {
            _result.Count().ShouldBe(1);
            _result.First().ShouldBe(true);
        }
    
    }
}
