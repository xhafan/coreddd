using System.IO;
using System.Linq;
using System.Reflection;
using Castle.Windsor;
using Core.Commons;
using NServiceBus;
using Rhino.Commons.Binsor;

namespace EmailMaker.Service
{
    public class EndpointConfig : AsA_Publisher, IConfigureThisEndpoint, IWantCustomLogging,
                                               IWantToRunAtStartup, IWantCustomInitialization
    {
        public IBus Bus { get; set; }

        public void Init()
        {
            var container = new WindsorContainer();

            var assembliesToLoad = new[]
                                       {
                                           "NServiceBus.dll",
                                           "NServiceBus.Core.dll",
                                           "EmailMaker.Messages.dll"
                                       };
            var assemblies = assembliesToLoad.Select(Assembly.LoadFrom);

            Configure
                .With(assemblies)
                .CastleWindsor25Builder(container)
                .BinarySerializer()
                .MsmqTransport()
                .MsmqSubscriptionStorage()
                .UnicastBus();

            BooReader.Read(container, "EmailMakerService.boo");
            IoC.Initialize(container);
        }


        public void Run()
        {
        }

        public void Stop()
        {
        }
    }
}
