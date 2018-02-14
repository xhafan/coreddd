using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NServiceBus;

namespace CoreNserviceBusTest.Extensions
{
    public static class BusExtensions
    {
        public static TMessage MessageShouldHaveBeenSent<TMessage>(this IBus bus) where TMessage : class, new()
        {
            object[] messages = null;
            A.CallTo(() => bus.Send(A<object[]>._)).MustHaveHappened();
            A.CallTo(() => bus.Send(A<object[]>._)).Invokes((object[] x) => messages = x);
            return messages[0] as TMessage;
        }

        // todo: implement this method
        public static IEnumerable<TMessage> MessagesShouldHaveBeenSentLocally<TMessage>(this IBus bus) where TMessage : class, new()
        {
            //var allMessages = bus.GetArgumentsForCallsMadeOn(a => a.SendLocal(Arg<object[]>.Is.Anything));
            //var flatAllMessages = allMessages.SelectMany(x => x).SelectMany(x => x as object[]);
            //return flatAllMessages.OfType<TMessage>();
            return null;
        }
    }
}
