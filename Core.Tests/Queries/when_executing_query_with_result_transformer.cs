using System.Collections.Generic;
using System.Linq;
using Castle.Windsor;
using Core.Infrastructure;
using Core.Queries;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace Core.Tests.Queries
{
    [TestFixture]
    public class when_executing_query_with_result_transformer
    {
        private IEnumerable<bool> _result;
        private IQueryMessageHandler<TestQueryMessage> _testQueryMessageHandler;
        private TestQueryMessage _testQueryMessage;

        public class TestQueryMessage : IQueryMessage { }

        public class Result {}

        [SetUp]
        public void Context()
        {
            var container = MockRepository.GenerateStub<IWindsorContainer>();
            IoC.Initialize(container);

            _testQueryMessageHandler = MockRepository.GenerateMock<IQueryMessageHandler<TestQueryMessage>>();
            _testQueryMessage = new TestQueryMessage();
            _testQueryMessageHandler.Expect(a => a.Execute<Result>(Arg<TestQueryMessage>.Matches(p => p == _testQueryMessage))).Return(new[] { new Result(), new Result() });

            container.Stub(a => a.Resolve<IQueryMessageHandler<TestQueryMessage>>()).Return(_testQueryMessageHandler);

            var queryExecutor = new QueryExecutor();
            _result = queryExecutor.Execute<TestQueryMessage, Result, bool>(_testQueryMessage, r => true);
        }

        [Test]
        public void query_was_executed_by_handler()
        {
            _testQueryMessageHandler.AssertWasCalled(a => a.Execute<Result>(_testQueryMessage));
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