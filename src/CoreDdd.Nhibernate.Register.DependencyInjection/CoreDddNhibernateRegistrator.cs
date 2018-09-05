using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;

namespace CoreDdd.Nhibernate.Register.DependencyInjection
{
    public static class CoreDddNhibernateRegistrator
    {
        public static void AddCoreDddNhibernate(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(NhibernateRepository<>));
            services.AddTransient(typeof(IRepository<,>), typeof(NhibernateRepository<,>));
            services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
            services.AddScoped<IUnitOfWork, NhibernateUnitOfWork>();
            // services.AddScoped<NhibernateUnitOfWork>(); // todo: replicate this line is needed
        }
    }
}