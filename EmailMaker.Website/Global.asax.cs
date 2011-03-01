using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Core;
using Castle.Windsor;
using Core.Commons;
using Core.Web;
using EmailMaker.Controllers.Template;
using Rhino.Commons.Binsor;
using Component = Castle.MicroKernel.Registration.Component;

namespace EmailMaker.Website
{
    public class MvcApplication : HttpApplication
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
            //BooReader.Read(container, "windsor.boo");
            _RegisterControllersInContainer(container);
            BooReader.Read(container, "windsor.boo");
            IoC.Initialize(container);

            ControllerBuilder.Current.SetControllerFactory(new IoCControllerFactory());
        }

        private void _RegisterControllersInContainer(IWindsorContainer container)
        {
//            var controllerTypes = from t in Assembly.GetAssembly(typeof(TemplateController)).GetTypes()
//                                  where typeof(IController).IsAssignableFrom(t)
//                                  select t;
//            foreach (var controllerType in controllerTypes)
//            {
//                container.Register(Component.For(controllerType).Named(controllerType.FullName).LifeStyle.Is(LifestyleType.Transient));
//            }            
        }
    }
}