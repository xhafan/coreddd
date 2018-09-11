using System;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.UnitOfWorks;
using Ninject.Modules;
using Ninject.Syntax;

namespace CoreDdd.Nhibernate.Register.Ninject
{
    public class CoreDddNhibernateBindings : NinjectModule
    {
        private static Func<IBindingInSyntax<NhibernateUnitOfWork>,
            IBindingNamedWithOrOnSyntax<NhibernateUnitOfWork>> _setUnitOfWorkLifeStyleFunc;

        public static void SetUnitOfWorkLifeStyle(
            Func<IBindingInSyntax<NhibernateUnitOfWork>,
                IBindingNamedWithOrOnSyntax<NhibernateUnitOfWork>> setLifeStyleFunc
        )
        {
            _setUnitOfWorkLifeStyleFunc = setLifeStyleFunc;
        }

        public override void Load()
        {
            if (_setUnitOfWorkLifeStyleFunc == null)
            {
                throw new Exception("First call CoreDddNhibernateBindings.SetUnitOfWorkLifeStyle() to set unit of work lifestyle " +
                                    "(e.g. CoreDddNhibernateBindings.SetUnitOfWorkLifeStyle(x => x.InRequestScope())");
            }

            Bind(typeof(IRepository<>)).To(typeof(NhibernateRepository<>)).InTransientScope();
            Bind(typeof(IRepository<,>)).To(typeof(NhibernateRepository<,>)).InTransientScope();

            Bind<IUnitOfWorkFactory>().To<UnitOfWorkFactory>().InSingletonScope();

            _setUnitOfWorkLifeStyleFunc(Bind<IUnitOfWork, NhibernateUnitOfWork>().To<NhibernateUnitOfWork>());
        }
    }
}
