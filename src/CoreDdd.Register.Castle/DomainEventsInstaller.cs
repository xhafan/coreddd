using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CoreDdd.Domain.Events;

namespace CoreDdd.Register.Castle
{
    public class DomainEventsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            AddTypedFactoryFacilityHelper.TryAddTypedFactoryFacility(container);
           
            container.Register(
                Component.For<IDomainEventHandlerFactory>().AsFactory()
            );
        }
    }
}