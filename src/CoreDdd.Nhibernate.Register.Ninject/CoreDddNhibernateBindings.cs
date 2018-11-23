using System;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.UnitOfWorks;
using Ninject.Modules;
using Ninject.Syntax;

namespace CoreDdd.Nhibernate.Register.Ninject
{
    /// <summary>
    /// Registers CoreDdd NHibernate services into Ninject IoC container.
    /// </summary>
    public class CoreDddNhibernateBindings : NinjectModule
    {
        private static Func<IBindingInSyntax<NhibernateUnitOfWork>,
            IBindingNamedWithOrOnSyntax<NhibernateUnitOfWork>> _setUnitOfWorkLifeStyleFunc;

        /// <summary>
        /// Sets NHibernate unit of work lifestyle.
        /// For a ASP.NET app, set the lifestyle per web request: CoreDddNhibernateBindings.SetUnitOfWorkLifeStyle(x => x.InRequestScope());
        /// </summary>
        /// <param name="setLifeStyleFunc">Set unit of work lifestyle func</param>
        public static void SetUnitOfWorkLifeStyle(
            Func<IBindingInSyntax<NhibernateUnitOfWork>,
                IBindingNamedWithOrOnSyntax<NhibernateUnitOfWork>> setLifeStyleFunc
        )
        {
            _setUnitOfWorkLifeStyleFunc = setLifeStyleFunc;
        }

        /// <summary>
        /// Registers the services.
        /// </summary>
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
