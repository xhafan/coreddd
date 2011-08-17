using Core.Queries;
using EmailMaker.Messages;
using EmailMaker.Service.Handlers;
using NUnit.Framework;
using Rhino.Mocks;

namespace EmailMaker.Service.Tests
{
    [TestFixture]
    public class when_handling_email_enqueued_to_be_sent_event_message
    {
        [SetUp]
        public void Context()
        {
            var queryExecutor = MockRepository.GenerateStub<IQueryExecutor>();
            var handler = new EmailEnqueuedToBeSentEventMessageHandler(queryExecutor);
            //handler.Handle(new EmailEnqueuedToBeSentEventMessage());
        }

        [Test]
        public void Test()
        {

        }
    }

}
