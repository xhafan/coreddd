using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Core.Commands;
using Core.Domain;
using Core.Domain.Persistence;
using Core.Infrastructure;
using Core.Queries;
using Core.Web;
using Core.Web.ModelBinders;
using EmailMaker.Commands;
using EmailMaker.Controllers;
using EmailMaker.Domain.EmailTemplates;
using EmailMaker.Domain.Emails;
using EmailMaker.Domain.Emails.EmailStates;
using EmailMaker.Domain.EventHandlers;
using EmailMaker.Dtos.Emails;
using EmailMaker.Infrastructure.Conventions;
using EmailMaker.Queries;
using NServiceBus;

namespace EmailMaker.Website
{
    public class UnitOfWorkApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);


            var container = new WindsorContainer();

            var binPath = Server.MapPath("~/bin");
            var assembliesToLoad = new[]
                                       {
                                           "NServiceBus.dll",
                                           "NServiceBus.Core.dll"
                                       };
            var assemblies = assembliesToLoad.Select(x => Assembly.LoadFrom(Path.Combine(binPath, x)));

            Configure
                .With(assemblies)
                .Log4Net()
                .CastleWindsorBuilder(container)
                .BinarySerializer()
                .MsmqTransport()
                .UnicastBus()
                .LoadMessageHandlers()
                .CreateBus()
                .Start();
            
            container.Install(
                FromAssembly.Containing<ControllerInstaller>()
                , FromAssembly.Containing<QueryExecutorInstaller>()
                , FromAssembly.Containing<CommandHandlerInstaller>()
                , FromAssembly.Containing<EventHandlerInstaller>()
                , FromAssembly.Containing<QueryMessageHandlerInstaller>()
                );
            IoC.Initialize(container);

            ControllerBuilder.Current.SetControllerFactory(new IoCControllerFactory());
            ModelBinders.Binders.DefaultBinder = new EnumConverterModelBinder();

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
            // todo: remove project reference to emailmaker.domain and core.domain
        }

        public virtual void Application_BeginRequest()
        {
            UnitOfWork.Current.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public virtual void Application_EndRequest()
        {
            if (!UnitOfWork.IsStarted) return;
            UnitOfWork.Current.TransactionalFlush();
            UnitOfWork.Current.Dispose();
        }

        public virtual void Application_Error()
        {
            if (!UnitOfWork.IsStarted) return;
            UnitOfWork.Current.TransactionalRollback();
            UnitOfWork.Current.Dispose();
        }
    
    }
}