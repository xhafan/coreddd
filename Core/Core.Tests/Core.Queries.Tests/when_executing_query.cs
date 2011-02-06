using System.Collections.Generic;
using System.Linq;
using Castle.Windsor;
using Core.Commons;
using NUnit.Framework;
using Rhino.Mocks;
using Shouldly;

namespace Core.Queries.Tests
{
    [TestFixture]
    public class when_executing_query
    {
        private IEnumerable<bool> _result;
        private IQueryMessageHandler<TestQueryMessage> _testQueryMessageHandler;
        private TestQueryMessage _testQueryMessage;

        public class TestQueryMessage : IQueryMessage {}
        
        [SetUp]
        public void Context()
        {
            var container = MockRepository.GenerateStub<IWindsorContainer>();
            IoC.Initialize(container);

            _testQueryMessageHandler = MockRepository.GenerateMock<IQueryMessageHandler<TestQueryMessage>>();
            _testQueryMessage = new TestQueryMessage();
            _testQueryMessageHandler.Expect(a => a.Execute<bool>(Arg<TestQueryMessage>.Matches(p => p == _testQueryMessage))).Return(new[] { true });

            container.Stub(a => a.Resolve<IQueryMessageHandler<TestQueryMessage>>()).Return(_testQueryMessageHandler);

            var queryExecutor = new QueryExecutor();
            _result = queryExecutor.Execute<TestQueryMessage, bool>(_testQueryMessage);
        }

        [Test]
        public void query_was_executed_by_handler()
        {
            _testQueryMessageHandler.AssertWasCalled(a => a.Execute<bool>(_testQueryMessage));            
        }

        [Test]
        public void result_is_correct()
        {
            _result.Count().ShouldBe(1);
            _result.First().ShouldBe(true);
        }
    
    }

}
