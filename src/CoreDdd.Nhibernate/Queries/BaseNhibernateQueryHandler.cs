using System;
using System.Collections.Generic;
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
        private const string NotImplementedExceptionMessage = "Override this method in the query handler.";

        /// <summary>
        /// NHibernate session.
        /// </summary>
        protected readonly ISession Session;

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="unitOfWork">An instance of NHibernate unit of work</param>
        protected BaseNhibernateQueryHandler(NhibernateUnitOfWork unitOfWork)
        {
            Session = unitOfWork.Session ?? throw new Exception("UnitOfWork Session is null");
        }

        /// <summary>
        /// Executes the query and returns a collection of results.
        /// </summary>
        /// <typeparam name="TResult">The query result type</typeparam>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A collection of query results</returns>
        public virtual IEnumerable<TResult> Execute<TResult>(TQuery query)
        {
            throw new NotImplementedException(NotImplementedExceptionMessage);
        }

        /// <summary>
        /// Executes the query and returns a single result.
        /// </summary>
        /// <typeparam name="TResult">A query result type</typeparam>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A single query result</returns>
        public virtual TResult ExecuteSingle<TResult>(TQuery query)
        {
            throw new NotImplementedException(NotImplementedExceptionMessage);
        }

#if !NET40
        /// <summary>
        /// Executes the query asynchronously and returns a collection of results.
        /// </summary>
        /// <typeparam name="TResult">The query result type</typeparam>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A collection of query results</returns>
        public virtual Task<IEnumerable<TResult>> ExecuteAsync<TResult>(TQuery query)
        {
            throw new NotImplementedException(NotImplementedExceptionMessage);
        }
        
        /// <summary>
        /// Executes the query asynchronously and returns a single result.
        /// </summary>
        /// <typeparam name="TResult">A query result type</typeparam>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A single query result</returns> 
        public virtual Task<TResult> ExecuteSingleAsync<TResult>(TQuery query)
        {
            throw new NotImplementedException(NotImplementedExceptionMessage);
        }
#endif
    }
}
