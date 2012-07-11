using System.Linq;
using System.Reflection;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Core.Commons;
using Core.Domain;
using Core.Domain.Persistence;
using Core.Queries;
using EmailMaker.DTO.Emails;
using EmailMaker.Domain.Conventions;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.Emails.EmailStates;
using EmailMaker.Domain.Services;
using EmailMaker.Queries;
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

            //BooReader.Read(container, "EmailMakerService.boo");
            container.Install(
                FromAssembly.Containing<QueryExecutorInstaller>()
                , FromAssembly.Containing<NhibernateRepositoryInstaller>()
                , FromAssembly.Containing<EmailHtmlBuilderInstaller>()
                , FromAssembly.Containing<EmailSenderInstaller>()
                , FromAssembly.Containing<QueryMessageHandlerInstaller>()
                );
            IoC.Initialize(container);

            // todo: unify initialization with a code in global.asax.cs
            var assembliesToMap = new[]
                                      {
                                          typeof (Email).Assembly,
                                          typeof (EmailDTO).Assembly
                                      }; 
            UnitOfWork.Initialize(
                new NHibernateConfigurator(
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
