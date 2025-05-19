using System.Collections.Generic;
using CoreDdd.Nhibernate.UnitOfWorks;
using NHibernate;
using IQuery = CoreDdd.Queries.IQuery;

#if !NET40
using System.Threading.Tasks;
#endif

#if NET45
using System;
#endif


namespace CoreDdd.Nhibernate.Queries
{
    /// <summary>
    /// Base query handler class for NHibernate QueryOver queries.
    /// </summary>
    /// <typeparam name="TQuery">A query type</typeparam>
    public abstract class BaseQueryOverHandler<TQuery> : BaseNhibernateQueryHandler<TQuery> where TQuery : IQuery
    {
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        /// <param name="unitOfWork">An instance of NHibernate unit of work</param>
        protected BaseQueryOverHandler(NhibernateUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {            
        }

        /// <summary>
        /// Gets a QueryOver query.
        /// </summary>
        /// <typeparam name="TResult">A query result type</typeparam>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A QueryOver instance</returns>
        protected abstract IQueryOver GetQueryOver<TResult>(TQuery query);

        /// <summary>
        /// Executes the QueryOver query.
        /// </summary>
        /// <typeparam name="TResult">A query result type</typeparam>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A collection of query results</returns>
        public override IEnumerable<TResult> Execute<TResult>(TQuery query)
        {
            return GetQueryOver<TResult>(query).UnderlyingCriteria.Future<TResult>();
        }

#if !NET40
        /// <summary>
        /// Executes the QueryOver query asynchronously.
        /// </summary>
        /// <typeparam name="TResult">A query result type</typeparam>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A collection of query results</returns>
        public override Task<IEnumerable<TResult>> ExecuteAsync<TResult>(TQuery query)
        {
#endif
#if NET40
#elif NET45
            throw _GetAsyncNotSupportedException();
#else
            return GetQueryOver<TResult>(query).UnderlyingCriteria.Future<TResult>().GetEnumerableAsync();
#endif
#if !NET40
        }
#endif

#if NET40
#elif NET45
        private NotSupportedException _GetAsyncNotSupportedException()
        {
            return new NotSupportedException(AsyncErrorMessageConstants.AsyncMethodNotSupportedExceptionMessage);
        }
#endif
    }
}