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

        public IQueryHandler<TQuery> Create<TQuery>() where TQuery : IQuery
        {
            return _ninjectIoCContainer.Get<IQueryHandler<TQuery>>();
        }

        public void Release<TQuery>(IQueryHandler<TQuery> queryHandler) where TQuery : IQuery
        {
            _ninjectIoCContainer.Release(queryHandler);
        }
    }
}