using CoreDdd.Commands;
using CoreDdd.Domain.Events;
using CoreDdd.Queries;
using Microsoft.Extensions.DependencyInjection;
    
namespace CoreDdd.Register.DependencyInjection
{
    public static class CoreDddRegistrator
    {
        public static void AddCoreDdd(this IServiceCollection services)
        {
            services.AddTransient<IQueryHandlerFactory, QueryHandlerFactory>();
            services.AddTransient<IQueryExecutor, QueryExecutor>();

            services.AddTransient<ICommandHandlerFactory, CommandHandlerFactory>();
            services.AddTransient<ICommandExecutor, CommandExecutor>();

            services.AddTransient<IDomainEventHandlerFactory, DomainEventHandlerFactory>();
        }
    }
}