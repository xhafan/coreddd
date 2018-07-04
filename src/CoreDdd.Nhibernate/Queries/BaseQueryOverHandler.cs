using System.Collections.Generic;
using CoreDdd.Nhibernate.UnitOfWorks;
using NHibernate;
using IQuery = CoreDdd.Queries.IQuery;
#if !NET40
using System;
using System.Threading.Tasks;
#endif

namespace CoreDdd.Nhibernate.Queries
{
    public abstract class BaseQueryOverHandler<TQuery> : BaseNhibernateQueryHandler<TQuery> where TQuery : IQuery
    {
        protected BaseQueryOverHandler(NhibernateUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {            
        }

        protected abstract IQueryOver GetQueryOver<TResult>(TQuery query);

        public override IEnumerable<TResult> Execute<TResult>(TQuery query)
        {
            return GetQueryOver<TResult>(query).UnderlyingCriteria.Future<TResult>();
        }

#if !NET40
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