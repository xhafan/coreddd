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
    : BaseNhibernateQueryHandler<GetTestEntityCountTestNhibernateQuery, int>(unitOfWork)
{
    public override IEnumerable<int> Execute(GetTestEntityCountTestNhibernateQuery query)
    {
        return Session.CreateCriteria<TestEntity>()
            .SetProjection(Projections.Count(Projections.Id()))
            .Future<int>();
    }
        
    public override int ExecuteSingle(GetTestEntityCountTestNhibernateQuery query)
    {
        return Session.CreateCriteria<TestEntity>()
            .SetProjection(Projections.Count(Projections.Id()))
            .FutureValue<int>().Value;
    }        

#if !NET40 && !NET45
    public override Task<IEnumerable<int>> ExecuteAsync(GetTestEntityCountTestNhibernateQuery query)
    {
        return Session.CreateCriteria<TestEntity>()
            .SetProjection(Projections.Count(Projections.Id()))
            .Future<int>().GetEnumerableAsync();
    }
    
    public override Task<int> ExecuteSingleAsync(GetTestEntityCountTestNhibernateQuery query)
    {
        return Session.CreateCriteria<TestEntity>()
            .SetProjection(Projections.Count(Projections.Id()))
            .FutureValue<int>().GetValueAsync();
    }    
#endif
}