using System;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Registration.Lifestyle;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.UnitOfWorks;

namespace CoreDdd.Nhibernate.Register.Castle
{
    /// <summary>
    /// Registers CoreDdd NHibernate services into Castle Windsor IoC container.
    /// </summary>
    public class CoreDddNhibernateInstaller : IWindsorInstaller
    {
        private static Func<LifestyleGroup<IUnitOfWork>, ComponentRegistration<IUnitOfWork>> _setUnitOfWorkLifeStyleFunc;

        /// <summary>
        /// Sets NHibernate unit of work lifestyle.
        /// For a ASP.NET app, set the lifestyle per web request: CoreDddNhibernateInstaller.SetUnitOfWorkLifeStyle(x => x.PerWebRequest);
        /// For a ASP.NET Core app, set the lifestyle as scoped: CoreDddNhibernateInstaller.SetUnitOfWorkLifeStyle(x => x.Scoped());
        /// For an app handling Rebus messages, set the lifestyle per Rebus message: CoreDddNhibernateInstaller.SetUnitOfWorkLifeStyle(x => x.PerRebusMessage());
        /// </summary>
        /// <param name="setLifeStyleFunc">Set unit of work lifestyle func</param>
        public static void SetUnitOfWorkLifeStyle(Func<LifestyleGroup<IUnitOfWork>, ComponentRegistration<IUnitOfWork>> setLifeStyleFunc)
        {
            _setUnitOfWorkLifeStyleFunc = setLifeStyleFunc;
        }

        /// <summary>
        /// Registers the services.
        /// </summary>
        /// <param name="container">Castle Windsor container</param>
        /// <param name="store">Castle Windsor configuration store</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (_setUnitOfWorkLifeStyleFunc == null)
            {
                throw new Exception("First call CoreDddNhibernateInstaller.SetUnitOfWorkLifeStyle() to set unit of work lifestyle " +
                                    "(e.g. CoreDddNhibernateInstaller.SetUnitOfWorkLifeStyle(x => x.PerWebRequest)");
            }

            AddTypedFactoryFacilityHelper.TryAddTypedFactoryFacility(container);

            container.Register(

                Component.For(typeof (IRepository<>))
                    .ImplementedBy(typeof (NhibernateRepository<>))
                    .LifeStyle.Transient,

                Component.For(typeof (IRepository<,>))
                    .ImplementedBy(typeof (NhibernateRepository<,>))
                    .LifeStyle.Transient,

                Component.For<IUnitOfWorkFactory>().AsFactory(),

                _setUnitOfWorkLifeStyleFunc(
                    Component.For<IUnitOfWork>()
                        .ImplementedBy<NhibernateUnitOfWork>()
                        .Forward<NhibernateUnitOfWork>()
                        .LifeStyle
                    )
                );
        }
    }
}