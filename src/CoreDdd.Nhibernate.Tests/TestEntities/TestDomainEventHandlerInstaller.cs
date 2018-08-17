using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CoreDdd.Domain.Events;

namespace CoreDdd.Nhibernate.Tests.TestEntities
{
    public class TestDomainEventHandlerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDomainEventHandler<TestDomainEvent>>()
                    .ImplementedBy<TestDomainEventHandler>()
                    .LifeStyle.Transient
            );
        }
    }
}