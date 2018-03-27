using CoreDdd.Queries;
using Ninject.Modules;

namespace CoreDdd.Register.Ninject
{
    public class QueryExecutorBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IQueryExecutor>().To<QueryExecutor>().InTransientScope();
        }
    }
}