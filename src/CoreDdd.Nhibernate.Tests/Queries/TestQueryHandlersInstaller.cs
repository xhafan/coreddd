using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CoreDdd.Queries;

namespace CoreDdd.Nhibernate.Tests.Queries
{
    public class TestQueryHandlersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IQueryHandler<GetTestEntityCountTestQueryOverQuery, int>>()
                    .ImplementedBy<GetTestEntityCountTestQueryOverQueryHandler>()
                    .LifeStyle.Transient,            
                Component.For<IQueryHandler<GetTestEntityCountTestNhibernateQuery, int>>()
                    .ImplementedBy<GetTestEntityCountTestNhibernateQueryHandler>()
                    .LifeStyle.Transient,
                Component.For<IQueryHandler<GetTestEntityCountTestAdoNetQuery, int>>()
                    .ImplementedBy<GetTestEntityCountTestAdoNetQueryHandler>()
                    .LifeStyle.Transient
            );
        }
    }
}