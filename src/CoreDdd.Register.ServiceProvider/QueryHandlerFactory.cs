using System;
using CoreDdd.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace CoreDdd.Register.DependencyInjection
{
    internal class QueryHandlerFactory : IQueryHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IQueryHandler<TQuery, TResult> Create<TQuery, TResult>() where TQuery : IQuery<TResult>
        {
            return _serviceProvider.GetService<IQueryHandler<TQuery, TResult>>();
        }

        public void Release<TQuery, TResult>(IQueryHandler<TQuery, TResult> queryHandler) where TQuery : IQuery<TResult>
        {
        }
    }
}