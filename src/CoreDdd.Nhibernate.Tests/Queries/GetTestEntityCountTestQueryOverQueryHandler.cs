using CoreDdd.Nhibernate.Queries;
using CoreDdd.Nhibernate.UnitOfWorks;
using IntegrationTestsShared.TestEntities;
using NHibernate;
using NHibernate.Criterion;

namespace CoreDdd.Nhibernate.Tests.Queries
{
    public class GetTestEntityCountTestQueryOverQueryHandler : BaseQueryOverHandler<GetTestEntityCountTestQueryOverQuery>
    {
        public GetTestEntityCountTestQueryOverQueryHandler(NhibernateUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override IQueryOver GetQueryOver<TResult>(GetTestEntityCountTestQueryOverQuery query)
        {
            return Session.QueryOver<TestEntity>().Select(Projections.RowCount());
        }
    }
}