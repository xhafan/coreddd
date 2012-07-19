using System.Collections.Generic;
using System.Linq;
using NServiceBus;
using Rhino.Mocks;

namespace Core.Tests.Helpers.Extensions
{
    public static class BusExtensions
    {
        public static TMessage MessageShouldHaveBeenSent<TMessage>(this IBus bus) where TMessage : class, new()
        {
            bus.AssertWasCalled(a => a.Send(Arg<object[]>.Is.Anything));
            return ((object[])bus.GetArgumentsForCallsMadeOn(a => a.Send(Arg<object[]>.Is.Anything))[0][0])[0] as TMessage;
        }

        public static IEnumerable<TMessage> MessagesShouldHaveBeenSentLocally<TMessage>(this IBus bus) where TMessage : class, new()
        {
            var allMessages = bus.GetArgumentsForCallsMadeOn(a => a.SendLocal(Arg<object[]>.Is.Anything));
            var flatAllMessages = allMessages.SelectMany(x => x).SelectMany(x => x as object[]);
            return flatAllMessages.OfType<TMessage>();
        }
    }
}
