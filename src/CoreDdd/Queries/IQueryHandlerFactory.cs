namespace CoreDdd.Queries
{
    /// <summary>
    /// A factory to create a query handler for a given query type.
    /// Usually implemented auto-magically by an IoC container.
    /// </summary>
    public interface IQueryHandlerFactory
    {
        /// <summary>
        /// Create an instance of a query handler for a given query type.
        /// </summary>
        /// <typeparam name="TQuery">A query type</typeparam>
        /// <typeparam name="TResult">A query result type</typeparam>
        /// <returns>An instance of a query handler</returns>
        IQueryHandler<TQuery, TResult> Create<TQuery, TResult>() where TQuery : IQuery<TResult>;

        /// <summary>
        /// Releases a query handler instance previously created by <see cref="Create{TQuery, TResult}"/> method.
        /// This is needed by Castle Windsor IoC container, other IoC containers (e.g. Ninject, ServiceProvider) don't support it.
        /// </summary>
        /// <typeparam name="TQuery">A query type</typeparam>
        /// <typeparam name="TResult">A query result type</typeparam>
        /// <param name="queryHandler">A query handler instance</param>
        void Release<TQuery, TResult>(IQueryHandler<TQuery, TResult> queryHandler) where TQuery : IQuery<TResult>;
    }
}