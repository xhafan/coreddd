using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks.NhibernateUnitOfWorkRunners;

[TestFixture]
public class when_running_operation
{
    private TestEntity _testEntity;

    [SetUp]
    public void Context()
    {
        NhibernateUnitOfWorkRunner.Run(
            IoC.Resolve<NhibernateUnitOfWork>,
            unitOfWork =>
            {
                var testEntityRepository = new NhibernateRepository<TestEntity>(unitOfWork);

                _testEntity = new TestEntity();
                testEntityRepository.Save(_testEntity);
            }
        );
    }

    [Test]
    public void test_entity_is_persisted()
    {
        using var unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
        unitOfWork.BeginTransaction();

        var testEntityRepository = new NhibernateRepository<TestEntity>(unitOfWork);
        testEntityRepository.Get(_testEntity.Id).ShouldNotBeNull();

        unitOfWork.Rollback();
    }

    [TearDown]
    public void TearDown()
    {
        using var unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
        unitOfWork.BeginTransaction();
        var testEntityRepository = new NhibernateRepository<TestEntity>(unitOfWork);
        var testEntity = testEntityRepository.Get(_testEntity.Id);
        if (testEntity != null)
        {
            testEntityRepository.Delete(testEntity);
        }
        unitOfWork.Commit();
    }
}