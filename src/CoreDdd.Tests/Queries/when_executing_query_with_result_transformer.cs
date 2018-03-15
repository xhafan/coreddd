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
    public class when_executing_query_with_result_transformer
    {
        private IEnumerable<int> _result;
        private IQueryHandler<TestQuery> _testQueryHandler;
        private TestQuery _testQuery;

        public class TestQuery : IQuery { }

        public class Result
        {
            public Result(int id)
            {
                Id = id;
            }

            public int Id { get; }
        }

        [SetUp]
        public void Context()
        {
            var container = A.Fake<IContainer>();
            IoC.Initialize(container);

            _testQueryHandler = A.Fake<IQueryHandler<TestQuery>>();
            _testQuery = new TestQuery();
            A.CallTo(() => _testQueryHandler.Execute<Result>(A<TestQuery>.That.Matches(p => p == _testQuery))).Returns(new[] { new Result(23), new Result(24) });

            A.CallTo(() => container.Resolve<IQueryHandler<TestQuery>>()).Returns(_testQueryHandler);

            var queryExecutor = new QueryExecutor();
            _result = queryExecutor.Execute<TestQuery, Result, int>(_testQuery, r => r.Id);
        }

        [Test]
        public void query_was_executed_by_handler()
        {
            A.CallTo(() => _testQueryHandler.Execute<Result>(_testQuery)).MustHaveHappened();
        }

        [Test]
        public void results_are_correctly_transformed()
        {
            _result.Count().ShouldBe(2);
            _result.First().ShouldBe(23);
            _result.Last().ShouldBe(24);
        }

    }
}