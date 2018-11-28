using System.Collections.Generic;
using CoreDdd.Nhibernate.Queries;
using CoreDdd.Nhibernate.UnitOfWorks;
using IntegrationTestsShared.TestEntities;
using NHibernate.Criterion;
#if !NET40 && !NET45
using System.Threading.Tasks;
#endif

namespace CoreDdd.Nhibernate.Tests.Queries
{
    public class GetTestEntityCountTestNhibernateQueryHandler : BaseNhibernateQueryHandler<GetTestEntityCountTestNhibernateQuery>
    {
        public GetTestEntityCountTestNhibernateQueryHandler(NhibernateUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override IEnumerable<TResult> Execute<TResult>(GetTestEntityCountTestNhibernateQuery query)
        {
            return Session.CreateCriteria<TestEntity>()
                .SetProjection(Projections.Count(Projections.Id()))
                .Future<TResult>();
        }

#if !NET40 && !NET45
        public override Task<IEnumerable<TResult>> ExecuteAsync<TResult>(GetTestEntityCountTestNhibernateQuery query)
        {
            return Session.CreateCriteria<TestEntity>()
                .SetProjection(Projections.Count(Projections.Id()))
                .Future<TResult>().GetEnumerableAsync();
        }
#endif
    }
}