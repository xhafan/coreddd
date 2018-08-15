using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CoreDdd.Domain.Events;
using CoreUtils.Storages;

namespace CoreDdd.Register.Castle
{
    public class DomainEventsInstaller : IWindsorInstaller // todo: add ninject registration
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            AddTypedFactoryFacilityHelper.TryAddTypedFactoryFacility(container);
           
            container.Register(
                Component.For<IDomainEventHandlerFactory>().AsFactory(),
                Component.For<IStorageFactory>().AsFactory()
            );
        }
    }
}