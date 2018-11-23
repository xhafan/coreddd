using CoreDdd.Commands;
using CoreDdd.Domain.Events;
using CoreDdd.Queries;
using Microsoft.Extensions.DependencyInjection;
    
namespace CoreDdd.Register.DependencyInjection
{
    /// <summary>
    /// Registers CoreDdd services into Dependency Injection Service Provider IoC container.
    /// </summary>
    public static class CoreDddRegistrator
    {
        /// <summary>
        /// Registers the services.
        /// </summary>
        /// <param name="services">A service collection</param>
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