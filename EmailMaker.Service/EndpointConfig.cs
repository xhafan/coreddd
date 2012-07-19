using System.Linq;
using System.Reflection;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Core.Domain;
using Core.Domain.Persistence;
using Core.Infrastructure;
using Core.Queries;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.Emails.EmailStates;
using EmailMaker.Dtos.Emails;
using EmailMaker.Infrastructure.Conventions;
using EmailMaker.Queries;
using EmailMaker.Service.Handlers;
using NServiceBus;

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
                .CastleWindsorBuilder(container)
                .BinarySerializer()
                .MsmqTransport()
                .MsmqSubscriptionStorage()
                .UnicastBus();

            container.Install(
                FromAssembly.Containing<QueryExecutorInstaller>()
                , FromAssembly.Containing<EmailSenderInstaller>()
                , FromAssembly.Containing<QueryMessageHandlerInstaller>()
                );
            IoC.Initialize(container);

            // todo: unify initialization with a code in global.asax.cs
            var assembliesToMap = new[]
                                      {
                                          typeof (Email).Assembly,
                                          typeof (EmailDto).Assembly
                                      }; 
            UnitOfWork.Initialize(
                new NhibernateConfigurator(
                    assembliesToMap,
                    new[]
                        {
                            typeof (Identity<>),
                            typeof (EmailPart),
                            typeof (EmailState),
                            typeof (EmailTemplatePart)
                        },
                    new[]
                        {
                            typeof (EmailState)
                        },
                    typeof(SubclassConvention).Assembly)
                );
        }
    }
}
