using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NServiceBus;

namespace CoreNserviceBusTest.Extensions
{
    public static class BusExtensions
    {
        public static void ExpectMessageSent<TMessage>(this IBus bus, Action<TMessage> onSend) 
            where TMessage : class, new()
        {
            A.CallTo(() => bus.Send(A<object[]>._))
                .Invokes((object[] messages) =>
                {
                    onSend(messages.OfType<TMessage>().SingleOrDefault());
                });
        }

        public static void ExpectMessagesSentLocally<TMessage>(
            this IBus bus, 
            Action<IEnumerable<TMessage>> onSend
        )
            where TMessage : class, new()
        {
            A.CallTo(() => bus.SendLocal(A<object[]>._))
                .Invokes((object[] messages) =>
                {
                    onSend(messages.OfType<TMessage>());
                });
        }
    }
}
