using CoreDdd.Commands;
using CoreDdd.Domain.Events;
using CoreDdd.Queries;
using Ninject.Modules;

namespace CoreDdd.Register.Ninject
{
    public class CoreDddBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommandHandlerFactory>().To<CommandHandlerFactory>().InSingletonScope();
            Bind<ICommandExecutor>().To<CommandExecutor>().InTransientScope();

            Bind<IQueryHandlerFactory>().To<QueryHandlerFactory>().InSingletonScope();
            Bind<IQueryExecutor>().To<QueryExecutor>().InTransientScope();

            Bind<IDomainEventHandlerFactory>().To<DomainEventHandlerFactory>().InSingletonScope();
        }
    }
}