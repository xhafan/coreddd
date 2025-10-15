#if !NET40 && !NET45
using System;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks.NhibernateUnitOfWorkRunners;

[TestFixture]
public class when_running_async_operation_with_return_value_which_throws
{
    private TestEntity _testEntity;
    private Exception _ex;

    [SetUp]
    public void Context()
    {
        _ex = Should.Throw<Exception>(async () =>
        {
            await NhibernateUnitOfWorkRunner.RunAsync<TestEntity>(
                IoC.Resolve<NhibernateUnitOfWork>,
                async unitOfWork =>
                {
                    var testEntityRepository = new NhibernateRepository<TestEntity>(unitOfWork);

                    _testEntity = new TestEntity();
                    await testEntityRepository.SaveAsync(_testEntity);

                    throw new Exception("Test exception");
                }
            );
        });
    }

    [Test]
    public void test_entity_is_not_persisted()
    {
        using var unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
        unitOfWork.BeginTransaction();

        var testEntityRepository = new NhibernateRepository<TestEntity>(unitOfWork);
        testEntityRepository.Get(_testEntity.Id).ShouldBeNull();

        unitOfWork.Rollback();
    }

    [Test]
    public void exception_is_correct()
    {
        _ex.Message.ShouldBe("Test exception");
    }
}
#endif