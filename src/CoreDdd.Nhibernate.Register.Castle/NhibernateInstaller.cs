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
    // todo: rename this to CoreDddInstaller
    public class NhibernateInstaller : IWindsorInstaller
    {
        private static Func<LifestyleGroup<IUnitOfWork>, ComponentRegistration<IUnitOfWork>> _setUnitOfWorkLifeStyleFunc;

        public static void SetUnitOfWorkLifeStyle(Func<LifestyleGroup<IUnitOfWork>, ComponentRegistration<IUnitOfWork>> setLifeStyleFunc)
        {
            _setUnitOfWorkLifeStyleFunc = setLifeStyleFunc;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (_setUnitOfWorkLifeStyleFunc == null)
            {
                throw new Exception("First call NhibernateInstaller.SetUnitOfWorkLifeStyle() to set unit of work lifestyle " +
                                    "(e.g. NhibernateInstaller.SetUnitOfWorkLifeStyle(x => x.PerWebRequest)");
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