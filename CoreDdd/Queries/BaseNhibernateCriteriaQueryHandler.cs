using System.Collections.Generic;
using NHibernate;

namespace CoreDdd.Queries
{
    public abstract class BaseNhibernateCriteriaQueryHandler<TQuery> : BaseNhibernateQueryHandler<TQuery> where TQuery : IQuery
    {
        public abstract ICriteria GetCriteria<TResult>(TQuery query);

        public override IEnumerable<TResult> Execute<TResult>(TQuery query)
        {
            return GetCriteria<TResult>(query).Future<TResult>();
        }
    }
}