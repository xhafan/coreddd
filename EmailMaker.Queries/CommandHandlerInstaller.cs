using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core.Queries;

namespace EmailMaker.Queries
{
    public class QueryMessageHandlerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly()
                    .BasedOn(typeof(IQueryMessageHandler<>))
                    .WithService.FirstInterface()
                    .Configure(x => x.LifestyleTransient()));
        }
    }
}