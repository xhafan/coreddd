using System.Collections.Generic;
using NHibernate;

namespace CoreDdd.Queries
{
    public abstract class BaseQueryOverHandler<TQuery> : BaseNhibernateQueryHandler<TQuery> where TQuery : IQuery
    {
        public abstract IQueryOver GetCriteria<TResult>(TQuery query);

        public override IEnumerable<TResult> Execute<TResult>(TQuery query)
        {
            return GetCriteria<TResult>(query).UnderlyingCriteria.Future<TResult>();
        }
    }
}