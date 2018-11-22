using System.Collections.Generic;
using System.Linq;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.Queries;
using NHibernate;
using IQuery = CoreDdd.Queries.IQuery;
#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Nhibernate.Queries
{
    /// <summary>
    /// Base NHibernate query handler.
    /// For NHibernate QueryOver queries, derive your query handler from <see cref="BaseQueryOverHandler{TQuery}"/>.
    /// For ADO.NET SQL queries, derive your query handler from <see cref="BaseAdoNetQueryHandler{TQuery}"/>.
    /// If none of these two base query handlers is good enough for you, derive from this class.
    /// </summary>
    /// <typeparam name="TQuery">A query type</typeparam>
    public abstract class BaseNhibernateQueryHandler<TQuery> : IQueryHandler<TQuery> where TQuery : IQuery
    {
        /// <summary>
        /// NHibernate session.
        /// </summary>
        protected ISession Session;

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="unitOfWork">An instance of NHibernate unit of work</param>
        protected BaseNhibernateQueryHandler(NhibernateUnitOfWork unitOfWork)
        {
            Session = unitOfWork.Session;
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <typeparam name="TResult">The query result type</typeparam>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A collection of query results</returns>
        public virtual IEnumerable<TResult> Execute<TResult>(TQuery query)
        {
            return Enumerable.Empty<TResult>();
        }

#if !NET40
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        /// <summary>
        /// Executes the query asynchronously.
        /// </summary>
        /// <typeparam name="TResult">The query result type</typeparam>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A collection of query results</returns>
        public virtual async Task<IEnumerable<TResult>> ExecuteAsync<TResult>(TQuery query)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return Enumerable.Empty<TResult>();
        }
#endif
    }
}