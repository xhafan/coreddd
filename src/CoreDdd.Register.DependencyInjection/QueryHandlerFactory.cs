using System;
using CoreDdd.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace CoreDdd.Register.DependencyInjection
{
    public class QueryHandlerFactory : IQueryHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IQueryHandler<TQuery> Create<TQuery>() where TQuery : IQuery
        {
            return _serviceProvider.GetService<IQueryHandler<TQuery>>();
        }

        public void Release<TQuery>(IQueryHandler<TQuery> queryHandler) where TQuery : IQuery
        {
        }
    }
}