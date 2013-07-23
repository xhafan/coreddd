using Castle.Windsor;
using Castle.Windsor.Installer;
using CoreDdd.Queries;
using CoreIoC;
using EmailMaker.Infrastructure;
using EmailMaker.Messages;
using EmailMaker.Queries;
using EmailMaker.Service.Handlers;
using NServiceBus;

namespace EmailMaker.Service
{
    public class EndpointConfig : AsA_Server, IConfigureThisEndpoint, IWantCustomInitialization
    {
        public void Init()
        {
            var nserviceBusAssemblies = new[]
                                            {
                                                typeof (IMessage).Assembly,
                                                typeof (Configure).Assembly,
                                                typeof (SendEmailForEmailRecipientMessage).Assembly,
                                                typeof (SendEmailForEmailRecipientMessageHandler).Assembly
                                            };
            SetLoggingLibrary.Log4Net(log4net.Config.XmlConfigurator.Configure);
            var container = new WindsorContainer();
            Configure
                .With(nserviceBusAssemblies)
                .CastleWindsorBuilder(container)
                .BinarySerializer()
                .MsmqTransport()
                .MsmqSubscriptionStorage()
                .UnicastBus();
            container.Install(
                FromAssembly.Containing<QueryExecutorInstaller>(),
                FromAssembly.Containing<EmailSenderInstaller>(),
                FromAssembly.Containing<QueryHandlerInstaller>()
                );
            IoC.Initialize(container);
            UnitOfWorkInitializer.Initialize();
        }
    }
}
