using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CoreDdd.Commands;
using CoreDdd.Domain.Events;
using CoreDdd.Queries;

namespace CoreDdd.Register.Castle
{
    /// <summary>
    /// Registers CoreDdd services into Castle Windsor IoC container.
    /// </summary>
    public class CoreDddInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Registers the services.
        /// </summary>
        /// <param name="container">Castle Windsor container</param>
        /// <param name="store">Castle Windsor configuration store</param>
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