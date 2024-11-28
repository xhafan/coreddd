#if !NET40 && !NET45
using CoreDdd.Nhibernate.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreIoC;
using IntegrationTestsShared.TestEntities;

#if NET8_0_OR_GREATER
using NHibernate;
#endif

using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.UnitOfWorks
{
    [TestFixture]
    public class when_rolling_back_unit_of_work_on_disconnected_transaction_async
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
#if NET40 || NET45 || NET461
                _unitOfWork.Session.Transaction.Dispose();
#else
                _unitOfWork.Session.GetCurrentTransaction().Dispose();
#endif
            }
        }

        [Test]
        public void rollback_does_not_throw()
        {
            Should.NotThrow(async() => await _unitOfWork.RollbackAsync());
        }
    }
}
#endif