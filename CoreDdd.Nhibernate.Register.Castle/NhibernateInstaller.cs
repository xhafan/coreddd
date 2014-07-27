using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Registration.Lifestyle;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.UnitOfWorks;
using CoreUtils;

namespace CoreDdd.Nhibernate.Register.Castle
{
    public class NhibernateInstaller : IWindsorInstaller
    {
        private static Func<LifestyleGroup<IUnitOfWork>, ComponentRegistration<IUnitOfWork>> _setUnitOfWorkLifeStyleFunc;

        public static void SetUnitOfWorkLifeStyle(Func<LifestyleGroup<IUnitOfWork>, ComponentRegistration<IUnitOfWork>> setLifeStyleFunc)
        {
            _setUnitOfWorkLifeStyleFunc = setLifeStyleFunc;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Guard.Hope(_setUnitOfWorkLifeStyleFunc != null, "Call first NhibernateInstaller.SetUnitOfWorkLifeStyle() to set unit of work lifestyle");

            container.Register(

                Component.For(typeof (IRepository<>))
                    .ImplementedBy(typeof (NhibernateRepository<>))
                    .LifeStyle.Transient,

                Component.For(typeof (IRepository<,>))
                    .ImplementedBy(typeof (NhibernateRepository<,>))
                    .LifeStyle.Transient,

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