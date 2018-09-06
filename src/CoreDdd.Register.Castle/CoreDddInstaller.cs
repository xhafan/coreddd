using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CoreDdd.Commands;
using CoreDdd.Domain.Events;
using CoreDdd.Queries;

namespace CoreDdd.Register.Castle
{
    public class CoreDddInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            AddTypedFactoryFacilityHelper.TryAddTypedFactoryFacility(container);

            container.Register(
                Component.For<ICommandHandlerFactory>().AsFactory(),
                Component.For<ICommandExecutor>()
                    .ImplementedBy<CommandExecutor>()
                    .LifeStyle.Transient);

            container.Register(
                Component.For<IQueryHandlerFactory>().AsFactory(),
                Component.For<IQueryExecutor>()
                    .ImplementedBy<QueryExecutor>()
                    .LifeStyle.Transient);

            container.Register(
                Component.For<IDomainEventHandlerFactory>().AsFactory()
            );
        }
    }
}