using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace EmailMaker.Controllers
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes
                                   .FromThisAssembly()
                                   .BasedOn<IController>()
                                   .Configure(x => x.LifestyleTransient()));
        }
    }
}