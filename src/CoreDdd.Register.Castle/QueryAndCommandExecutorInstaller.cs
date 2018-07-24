using System.Linq;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CoreDdd.Commands;
using CoreDdd.Queries;

namespace CoreDdd.Register.Castle
{
    public class QueryAndCommandExecutorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
	        _tryAddTypedFactoryFacility();
           
            container.Register(
                Component.For<IQueryHandlerFactory>().AsFactory(),
                Component.For<IQueryExecutor>()
                    .ImplementedBy<QueryExecutor>()
                    .LifeStyle.Transient);

            container.Register(
                Component.For<ICommandHandlerFactory>().AsFactory(),
                Component.For<ICommandExecutor>()
                    .ImplementedBy<CommandExecutor>()
                    .LifeStyle.Transient);

            void _tryAddTypedFactoryFacility()
            {
                var facilities = container.Kernel.GetFacilities();
                if (facilities.All(x => x.GetType() != typeof(TypedFactoryFacility)))
                {
                    container.AddFacility<TypedFactoryFacility>();
                }
            }
        }
    }
}