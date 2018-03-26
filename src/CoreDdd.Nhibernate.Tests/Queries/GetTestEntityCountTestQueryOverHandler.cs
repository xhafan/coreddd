using CoreDdd.Nhibernate.Queries;
using CoreDdd.Nhibernate.Tests.TestEntities;
using NHibernate;
using NHibernate.Criterion;

namespace CoreDdd.Nhibernate.Tests.Queries
{
    public class GetTestEntityCountTestQueryOverHandler : BaseQueryOverHandler<GetTestEntityCountTestQueryOverQuery>
    {
        protected override IQueryOver GetQueryOver<TResult>(GetTestEntityCountTestQueryOverQuery query)
        {
            return Session.QueryOver<TestEntity>().Select(Projections.RowCount());
        }
    }
}