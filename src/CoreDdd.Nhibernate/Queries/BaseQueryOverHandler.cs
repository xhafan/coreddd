using System.Collections.Generic;
using CoreDdd.Nhibernate.UnitOfWorks;
using NHibernate;
using CoreDdd.Queries;

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
    /// <typeparam name="TResult">A query result type</typeparam>
    public abstract class BaseQueryOverHandler<TQuery, TResult> : BaseNhibernateQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
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
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A QueryOver instance</returns>
        protected abstract IQueryOver GetQueryOver(TQuery query);

        /// <summary>
        /// Executes the QueryOver query and returns a collection of results.
        /// </summary>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A collection of query results</returns>
        public override IEnumerable<TResult> Execute(TQuery query)
        {
            return GetQueryOver(query).UnderlyingCriteria.Future<TResult>();
        }

        /// <summary>
        /// Executes the QueryOver query and returns a single result.
        /// </summary>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A single query result</returns>
        public override TResult ExecuteSingle(TQuery query)
        {
            return GetQueryOver(query).UnderlyingCriteria.FutureValue<TResult>().Value;
        }        

#if !NET40
        /// <summary>
        /// Executes the QueryOver query asynchronously and returns a collection of results.
        /// </summary>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A collection of query results</returns>
        public override Task<IEnumerable<TResult>> ExecuteAsync(TQuery query)
        {
#endif
#if NET40
#elif NET45
            throw _GetAsyncNotSupportedException();
#else
            return GetQueryOver(query).UnderlyingCriteria.Future<TResult>().GetEnumerableAsync();
#endif
#if !NET40
        }
#endif
        
#if !NET40
        /// <summary>
        /// Executes the QueryOver query asynchronously and returns a single result.
        /// </summary>
        /// <param name="query">An instance of a query with a data</param>
        /// <returns>A single query result</returns>
        public override Task<TResult> ExecuteSingleAsync(TQuery query)
        {
#endif
#if NET40
#elif NET45
            throw _GetAsyncNotSupportedException();
#else
            return GetQueryOver(query).UnderlyingCriteria.FutureValue<TResult>().GetValueAsync();
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