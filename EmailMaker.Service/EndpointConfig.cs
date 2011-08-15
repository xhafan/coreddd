using System.Linq;
using System.Reflection;
using Castle.Windsor;
using Core.Commons;
using NServiceBus;
using Rhino.Commons.Binsor;

namespace EmailMaker.Service
{
    public class EndpointConfig : AsA_Server, IConfigureThisEndpoint, IWantCustomInitialization
    {
        public void Init()
        {
            var container = new WindsorContainer();

            var assembliesToLoad = new[]
                                       {
                                           "NServiceBus.dll",
                                           "NServiceBus.Core.dll",
                                           "EmailMaker.Messages.dll",
                                           "EmailMaker.Service.dll"
                                       };
            var assemblies = assembliesToLoad.Select(Assembly.LoadFrom);

            SetLoggingLibrary.Log4Net(log4net.Config.XmlConfigurator.Configure);

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
    }
}
