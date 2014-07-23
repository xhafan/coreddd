using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CoreDdd.Domain.Repositories;
using CoreDdd.Infrastructure;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;

namespace CoreDdd.Nhibernate.Register.Castle
{
    public class NhibernateInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(

                Component.For(typeof (IRepository<>))
                    .ImplementedBy(typeof (NhibernateRepository<>))
                    .LifeStyle.Transient,

                Component.For(typeof (IRepository<,>))
                    .ImplementedBy(typeof (NhibernateRepository<,>))
                    .LifeStyle.Transient,
   
                Component.For<IUnitOfWork>()
                    .ImplementedBy<NhibernateUnitOfWork>()
                    .Forward<NhibernateUnitOfWork>()
                    .LifeStyle.PerThread
                );
        }
    }
}