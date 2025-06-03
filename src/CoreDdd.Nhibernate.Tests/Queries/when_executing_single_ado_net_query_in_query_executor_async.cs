#if !NET40 && !NET45
using System.Threading.Tasks;
using CoreDdd.Nhibernate.TestHelpers;
using CoreDdd.Queries;
using CoreIoC;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Queries;

[TestFixture]
public class when_executing_single_ado_net_query_in_query_executor_async : BaseIoCPersistenceTest
{
    private int _result;
    private GetTestEntityCountTestAdoNetQuery _query;
      
    [SetUp]
    public async Task Context()
    {
        _persistTestEntity();
        _query = new GetTestEntityCountTestAdoNetQuery();

        var queryExecutor = IoC.Resolve<IQueryExecutor>();
        _result = await queryExecutor.ExecuteSingleAsync<GetTestEntityCountTestAdoNetQuery, int>(_query);

        void _persistTestEntity()
        {
            var testEntityOne = new TestEntity();
            UnitOfWork.Save(testEntityOne);
            var testEntityTwo = new TestEntity();
            UnitOfWork.Save(testEntityTwo);
        }
    }

    [Test]
    public void two_entities_are_counted()
    {
        _result.ShouldBe(2);
    }    
}
#endif