using System.Collections.Generic;
using System.Linq;
using Castle.Windsor;
using CoreDdd.Infrastructure;
using CoreDdd.Queries;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace CoreDdd.Tests.Queries
{
    [TestFixture]
    public class when_executing_query_with_result_transformer
    {
        private IEnumerable<bool> _result;
        private IQueryHandler<TestQuery> _testQueryHandler;
        private TestQuery _testQuery;

        public class TestQuery : IQuery { }

        public class Result {}

        [SetUp]
        public void Context()
        {
            var container = MockRepository.GenerateStub<IWindsorContainer>();
            IoC.Initialize(container);

            _testQueryHandler = MockRepository.GenerateMock<IQueryHandler<TestQuery>>();
            _testQuery = new TestQuery();
            _testQueryHandler.Expect(a => a.Execute<Result>(Arg<TestQuery>.Matches(p => p == _testQuery))).Return(new[] { new Result(), new Result() });

            container.Stub(a => a.Resolve<IQueryHandler<TestQuery>>()).Return(_testQueryHandler);

            var queryExecutor = new QueryExecutor();
            _result = queryExecutor.Execute<TestQuery, Result, bool>(_testQuery, r => true);
        }

        [Test]
        public void query_was_executed_by_handler()
        {
            _testQueryHandler.AssertWasCalled(a => a.Execute<Result>(_testQuery));
        }

        [Test]
        public void result_is_correct()
        {
            _result.Count().ShouldBe(2);
            _result.First().ShouldBe(true);
            _result.Last().ShouldBe(true);
        }

    }
}