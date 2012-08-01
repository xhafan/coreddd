using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Core.Domain.Repositories
{
    public class NhibernateRepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(NhibernateRepository<>))
                    .LifeStyle.Transient,
                Component.For(typeof(IRepository<,>))
                    .ImplementedBy(typeof(NhibernateRepository<,>))
                    .LifeStyle.Transient);
        }
    }
}