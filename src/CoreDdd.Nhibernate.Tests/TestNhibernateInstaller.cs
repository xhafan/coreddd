using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CoreDdd.Nhibernate.Configurations;

namespace CoreDdd.Nhibernate.Tests
{
    public class TestNhibernateInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<INhibernateConfigurator>()
                    .ImplementedBy<TestNhibernateConfigurator>()
                    .LifeStyle.Singleton
            );
        }
    }
}