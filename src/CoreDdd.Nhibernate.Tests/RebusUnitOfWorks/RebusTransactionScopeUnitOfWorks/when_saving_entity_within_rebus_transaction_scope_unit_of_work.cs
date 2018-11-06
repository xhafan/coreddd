#if !NET40 && !NET45
using System.Transactions;
using CoreDdd.Domain.Events;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.Tests.TestEntities;
using CoreDdd.Nhibernate.Tests.Webs;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.Rebus.UnitOfWork;
using CoreDdd.TestHelpers.DomainEvents;
using CoreDdd.UnitOfWorks;
using CoreIoC;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.RebusUnitOfWorks.RebusTransactionScopeUnitOfWorks
{
    [TestFixture]
    public class when_saving_entity_within_rebus_transaction_scope_unit_of_work
    {
        private VolatileResourceManager _volatileResourceManager;
        private IRepository<TestEntityWithDomainEvent> _entityRepository;
        private TestEntityWithDomainEvent _entity;
        private TestDomainEvent _raisedDomainEvent;
        private FakeMessageContext _fakeMessageContext;
        private (TransactionScope transactionScope, IUnitOfWork unitOfWork) _transactionScopeUnitOfWork;

        [SetUp]
        public void Context()
        {
            var domainEventHandlerFactory = new FakeDomainEventHandlerFactory(domainEvent => _raisedDomainEvent = (TestDomainEvent)domainEvent);
            DomainEvents.Initialize(domainEventHandlerFactory);

            _volatileResourceManager = new VolatileResourceManager();

            var unitOfWorkFactory = IoC.Resolve<IUnitOfWorkFactory>();
            RebusTransactionScopeUnitOfWork.Initialize(
                unitOfWorkFactory: unitOfWorkFactory,
                isolationLevel: IsolationLevel.ReadCommitted,
                transactionScopeEnlistmentAction: transactionScope => _volatileResourceManager.EnlistIntoTransactionScope(transactionScope)
                );
            _fakeMessageContext = new FakeMessageContext();
            _transactionScopeUnitOfWork = RebusTransactionScopeUnitOfWork.Create(_fakeMessageContext);

            _simulateApplicationTransaction();

            RebusTransactionScopeUnitOfWork.Commit(_fakeMessageContext, _transactionScopeUnitOfWork);
            RebusTransactionScopeUnitOfWork.Cleanup(_fakeMessageContext, _transactionScopeUnitOfWork);
        }

        private void _simulateApplicationTransaction()
        {
            _entityRepository = IoC.Resolve<IRepository<TestEntityWithDomainEvent>>();

            _entity = new TestEntityWithDomainEvent();
            _entity.BehaviouralMethodWithRaisingDomainEvent();

            _entityRepository.Save(_entity);

            _volatileResourceManager.SetMemberValue(23);
        }

        [Test]
        public void entity_is_persisted_and_can_be_fetched_after_the_transaction_commit()
        {
            _entity.ShouldNotBeNull();

            var unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
            unitOfWork.BeginTransaction();

            _entityRepository = IoC.Resolve<IRepository<TestEntityWithDomainEvent>>();
            _entity = _entityRepository.Get(_entity.Id);

            _entity.ShouldNotBeNull();

            unitOfWork.Rollback();
        }

        [Test]
        public void volatile_resource_manager_value_is_set_after_transaction_scope_commit()
        {
            _volatileResourceManager.MemberValue.ShouldBe(23);
        }

        [Test]
        public void domain_event_is_handled()
        {
            _raisedDomainEvent.ShouldNotBeNull();
        }
    }
}
#endif