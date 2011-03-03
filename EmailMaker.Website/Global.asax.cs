using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Core.Commons;
using Core.Web;
using Rhino.Commons.Binsor;

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
            BooReader.Read(container, "windsor.boo");
            IoC.Initialize(container);

            ControllerBuilder.Current.SetControllerFactory(new IoCControllerFactory());
        }

        public virtual void Application_BeginRequest()
        {
            UnitOfWork.Current.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public virtual void Application_EndRequest()
        {
            UnitOfWork.Current.TransactionalFlush();
            UnitOfWork.Current.Dispose();
        }
    }
}