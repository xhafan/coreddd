using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;
using System;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks.RollingBack;

[TestFixture]
public class when_rolling_back_active_unit_of_work_via_dispose
{
    private NhibernateUnitOfWork _unitOfWork;
    private TestEntity _testEntity;
    private NhibernateRepository<TestEntity> _testEntityRepository;
    private InvalidOperationException _ex;

    [SetUp]
    public void Context()
    {
        _unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
        _unitOfWork.BeginTransaction();

        _testEntityRepository = new NhibernateRepository<TestEntity>(_unitOfWork);

        _testEntity = new TestEntity();
        _testEntityRepository.Save(_testEntity);

        _ex = Should.Throw<InvalidOperationException>(() => _unitOfWork.Dispose());
    }

    [Test]
    public void exception_is_thrown()
    {
        _ex.Message.ShouldBe("Unit of work disposed without committing or rolling back.");
    }

    [Test]
    public void entities_are_not_persisted()
    {
        _unitOfWork.BeginTransaction();
        _testEntity = _testEntityRepository.Get(_testEntity.Id);

        _testEntity.ShouldBeNull();

        _unitOfWork.Rollback();
    }

    [Test]
    public void nhibernate_session_is_closed()
    {
        _unitOfWork.Session.ShouldBeNull();            
    }
}