using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CoreDdd.Nhibernate.Configurations;

namespace EmailMaker.Infrastructure
{
    public class EmailMakerNhibernateInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<INhibernateConfigurator>()
                    .ImplementedBy<EmailMakerNhibernateConfigurator>()
                    .LifeStyle.Singleton
                );
        }
    }
}