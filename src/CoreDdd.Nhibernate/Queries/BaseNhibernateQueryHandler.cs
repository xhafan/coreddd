using System;
using System.Collections.Generic;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.Queries;
using NHibernate;
#if !NET40
using System.Threading.Tasks;
#endif

namespace CoreDdd.Nhibernate.Queries
{
    /// <summary>
    /// Base NHibernate query handler.
    /// For NHibernate QueryOver queries, derive your query handler from <see cref="BaseQueryOverHandler{TQuery, TResult}"/>.
    /// For ADO.NET SQL queries, derive your query handler from <see cref="BaseAdoNetQueryHandler{TQuery, TResult}"/>.
    /// If none of these two base query handlers is good enough for you, derive from this class.
    /// </summary>
    /// <typeparam name="TQuery">A query type</typeparam>
    /// <typeparam name="TResult">A query result type</typeparam>
    public abstract class BaseNhibernateQueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
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
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A collection of query results</returns>
        public virtual IEnumerable<TResult> Execute(TQuery query)
        {
            throw new NotImplementedException(NotImplementedExceptionMessage);
        }

        /// <summary>
        /// Executes the query and returns a single result.
        /// </summary>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A single query result</returns>
        public virtual TResult ExecuteSingle(TQuery query)
        {
            throw new NotImplementedException(NotImplementedExceptionMessage);
        }

#if !NET40
        /// <summary>
        /// Executes the query asynchronously and returns a collection of results.
        /// </summary>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A collection of query results</returns>
        public virtual Task<IEnumerable<TResult>> ExecuteAsync(TQuery query)
        {
            throw new NotImplementedException(NotImplementedExceptionMessage);
        }
        
        /// <summary>
        /// Executes the query asynchronously and returns a single result.
        /// </summary>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A single query result</returns> 
        public virtual Task<TResult> ExecuteSingleAsync(TQuery query)
        {
            throw new NotImplementedException(NotImplementedExceptionMessage);
        }
#endif
    }
}
