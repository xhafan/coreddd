using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.Configurations;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;

namespace CoreDdd.Nhibernate.Register.DependencyInjection
{
    public static class CoreDddNhibernateRegistrator
    {
        public static void AddCoreDddNhibernate<TNhibernateConfigurator>(this IServiceCollection services)
            where TNhibernateConfigurator : class, INhibernateConfigurator
        {
            services.AddSingleton<INhibernateConfigurator, TNhibernateConfigurator>();
            services.AddTransient(typeof(IRepository<>), typeof(NhibernateRepository<>));
            services.AddTransient(typeof(IRepository<,>), typeof(NhibernateRepository<,>));
            services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
            services.AddScoped<IUnitOfWork, NhibernateUnitOfWork>();
            services.AddScoped(x => (NhibernateUnitOfWork)x.GetService<IUnitOfWork>());
        }
    }
}