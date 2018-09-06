using CoreDdd.Commands;
using CoreDdd.Domain.Events;
using CoreDdd.Queries;
using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace CoreDdd.Register.Ninject
{
    public class CoreDddBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommandHandlerFactory>().ToFactory();
            Bind<ICommandExecutor>().To<CommandExecutor>().InTransientScope();

            Bind<IQueryHandlerFactory>().ToFactory();
            Bind<IQueryExecutor>().To<QueryExecutor>().InTransientScope();

            Bind<IDomainEventHandlerFactory>().ToFactory();
        }
    }
}