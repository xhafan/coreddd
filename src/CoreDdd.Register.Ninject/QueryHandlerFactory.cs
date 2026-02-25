using CoreDdd.Queries;
using Ninject;
using Ninject.Syntax;

namespace CoreDdd.Register.Ninject
{
    internal class QueryHandlerFactory : IQueryHandlerFactory
    {
        private readonly IResolutionRoot _ninjectIoCContainer;

        public QueryHandlerFactory(IResolutionRoot ninjectIoCContainer)
        {
            _ninjectIoCContainer = ninjectIoCContainer;
        }

        public IQueryHandler<TQuery, TResult> Create<TQuery, TResult>() where TQuery : IQuery<TResult>
        {
            return _ninjectIoCContainer.Get<IQueryHandler<TQuery, TResult>>();
        }

        public void Release<TQuery, TResult>(IQueryHandler<TQuery, TResult> queryHandler) where TQuery : IQuery<TResult>
        {
            _ninjectIoCContainer.Release(queryHandler);
        }
    }
}