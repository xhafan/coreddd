using CoreDdd.Queries;
using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace CoreDdd.Register.Ninject
{
    public class QueryExecutorBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IQueryHandlerFactory>().ToFactory();
            Bind<IQueryExecutor>().To<QueryExecutor>().InTransientScope();
        }
    }
}