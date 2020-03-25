#if !NET40 && !NET45
using System;
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using IntegrationTestsShared.TestEntities;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks.Committing
{
    [TestFixture]
    public class when_committing_unit_of_work_on_disconnected_transaction_async
    {
        private NhibernateUnitOfWork _unitOfWork;
        private TestEntity _testEntity;
        private NhibernateRepository<TestEntity> _testEntityRepository;

        [SetUp]
        public void Context()
        {
            _unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
            _unitOfWork.BeginTransaction();

            _testEntityRepository = new NhibernateRepository<TestEntity>(_unitOfWork);

            _testEntity = new TestEntity();
            _testEntityRepository.Save(_testEntity);

            _makeTransactionDisconnected();

            void _makeTransactionDisconnected()
            {
                _unitOfWork.Session.Transaction.Dispose();
            }
        }

        [Test]
        public void transaction_rollback_within_commit_does_not_throw()
        {
            var ex = Should.Throw<Exception>(async () => await _unitOfWork.CommitAsync());
            ex.Message.ShouldBe("Transaction not successfully started");
            ex.StackTrace.ShouldNotContain("RollbackAsync(");
        }
    }
}
#endif