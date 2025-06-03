using System.Collections.Generic;
using CoreDdd.Nhibernate.Queries;
using CoreDdd.Nhibernate.UnitOfWorks;
using IntegrationTestsShared.TestEntities;
using NHibernate.Criterion;
#if !NET40 && !NET45
using System.Threading.Tasks;
#endif

namespace CoreDdd.Nhibernate.Tests.Queries;

public class GetTestEntityCountTestNhibernateQueryHandler(NhibernateUnitOfWork unitOfWork)
    : BaseNhibernateQueryHandler<GetTestEntityCountTestNhibernateQuery>(unitOfWork)
{
    public override IEnumerable<TResult> Execute<TResult>(GetTestEntityCountTestNhibernateQuery query)
    {
        return Session.CreateCriteria<TestEntity>()
            .SetProjection(Projections.Count(Projections.Id()))
            .Future<TResult>();
    }
        
    public override TResult ExecuteSingle<TResult>(GetTestEntityCountTestNhibernateQuery query)
    {
        return Session.CreateCriteria<TestEntity>()
            .SetProjection(Projections.Count(Projections.Id()))
            .FutureValue<TResult>().Value;
    }        

#if !NET40 && !NET45
    public override Task<IEnumerable<TResult>> ExecuteAsync<TResult>(GetTestEntityCountTestNhibernateQuery query)
    {
        return Session.CreateCriteria<TestEntity>()
            .SetProjection(Projections.Count(Projections.Id()))
            .Future<TResult>().GetEnumerableAsync();
    }
    
    public override Task<TResult> ExecuteSingleAsync<TResult>(GetTestEntityCountTestNhibernateQuery query)
    {
        return Session.CreateCriteria<TestEntity>()
            .SetProjection(Projections.Count(Projections.Id()))
            .FutureValue<TResult>().GetValueAsync();
    }    
#endif
}