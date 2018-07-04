namespace CoreDdd.Queries
{
    public interface IQueryHandlerFactory
    {
        IQueryHandler<TQuery> Create<TQuery>() where TQuery : IQuery;
        void Release<TQuery>(IQueryHandler<TQuery> queryHandler) where TQuery : IQuery;
    }
}