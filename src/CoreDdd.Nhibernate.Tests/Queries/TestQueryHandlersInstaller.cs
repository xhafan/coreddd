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
                Component.For<IQueryHandler<GetTestEntityCountTestQueryOverQuery>>()
                    .ImplementedBy<GetTestEntityCountTestQueryOverQueryHandler>()
                    .LifeStyle.Transient,            
                Component.For<IQueryHandler<GetTestEntityCountTestNhibernateQuery>>()
                    .ImplementedBy<GetTestEntityCountTestNhibernateQueryHandler>()
                    .LifeStyle.Transient,
                Component.For<IQueryHandler<GetTestEntityCountTestAdoNetQuery>>()
                    .ImplementedBy<GetTestEntityCountTestAdoNetQueryHandler>()
                    .LifeStyle.Transient
            );
        }
    }
}