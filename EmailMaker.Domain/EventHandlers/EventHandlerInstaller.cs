using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core.Domain.Events;

namespace EmailMaker.Domain.EventHandlers
{
    public class EventHandlerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly()
                    .BasedOn(typeof(IDomainEventHandler<>))
                    .WithService.FirstInterface()
                    .Configure(x => x.LifestyleTransient()));
        }
    }
}