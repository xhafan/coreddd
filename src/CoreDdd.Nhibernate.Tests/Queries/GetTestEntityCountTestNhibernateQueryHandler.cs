using System.Collections.Generic;
using CoreDdd.Nhibernate.Queries;
using CoreDdd.Nhibernate.Tests.TestEntities;
using NHibernate.Criterion;

namespace CoreDdd.Nhibernate.Tests.Queries
{
    public class GetTestEntityCountTestNhibernateQueryHandler : BaseNhibernateQueryHandler<GetTestEntityCountTestNhibernateQuery>
    {
        public override IEnumerable<TResult> Execute<TResult>(GetTestEntityCountTestNhibernateQuery query)
        {
            return Session.CreateCriteria<TestEntity>()
                .SetProjection(Projections.Count(Projections.Id()))
                .Future<TResult>();
        }
    }
}