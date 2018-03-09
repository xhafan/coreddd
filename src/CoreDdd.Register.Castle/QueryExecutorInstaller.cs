using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CoreDdd.Queries;

namespace CoreDdd.Register.Castle
{
    public class QueryExecutorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IQueryExecutor>()
                    .ImplementedBy<QueryExecutor>()
                    .LifeStyle.Transient);
        }
    }
}