using System;
using CoreDdd.Nhibernate.Repositories;
using IntegrationTestsShared;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Repositories;

[TestFixture]
public class when_loading_non_existent_aggregate_root_entity_by_id : BasePersistenceTest
{
    [Test]
    public void entity_is_persisted_and_retrieved()
    {
        var testEntityRepository = new NhibernateRepository<TestEntity>(UnitOfWork);
        var ex = Should.Throw<Exception>(() => testEntityRepository.LoadById(-1));

        ex.Message.ShouldBe("TestEntity Id -1 not found.");
    }
}