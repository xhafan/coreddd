using NServiceBus;
using Rhino.Mocks;

namespace Core.TestHelper.Extensions
{
    public static class BusExtensions
    {
        public static TMessage MessageShouldHaveBeenSent<TMessage>(this IBus bus) where TMessage : class, IMessage, new()
        {
            bus.AssertWasCalled(a => a.Send(Arg<IMessage[]>.Is.Anything));
            return ((IMessage[])bus.GetArgumentsForCallsMadeOn(a => a.Send(Arg<IMessage[]>.Is.Anything))[0][0])[0] as TMessage;
        }
    }
}
