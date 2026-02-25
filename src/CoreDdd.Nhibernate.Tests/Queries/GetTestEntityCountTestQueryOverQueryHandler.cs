using CoreDdd.Nhibernate.Queries;
using CoreDdd.Nhibernate.UnitOfWorks;
using IntegrationTestsShared.TestEntities;
using NHibernate;
using NHibernate.Criterion;

namespace CoreDdd.Nhibernate.Tests.Queries;

public class GetTestEntityCountTestQueryOverQueryHandler(NhibernateUnitOfWork unitOfWork)
    : BaseQueryOverHandler<GetTestEntityCountTestQueryOverQuery, int>(unitOfWork)
{
    protected override IQueryOver GetQueryOver(GetTestEntityCountTestQueryOverQuery query)
    {
        return Session.QueryOver<TestEntity>().Select(Projections.RowCount());
    }
}