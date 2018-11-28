using System.Web;
using CoreDdd.AspNet.HttpModules;
using CoreDdd.Domain.Events;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.TestHelpers.DomainEvents;
using CoreDdd.TestHelpers.HttpContexts;
using CoreDdd.UnitOfWorks;
using CoreIoC;
using IntegrationTestsShared.TestEntities;
using IntegrationTestsShared.TransactionScopes;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.AspNet.Tests.HttpModules.TransactionScopeUnitOfWorkHttpModules
{
    [TestFixture]
    public class when_saving_entity_within_transaction_scope_unit_of_work
    {
        private VolatileResourceManager _volatileResourceManager;
        private IRepository<TestEntityWithDomainEvent> _entityRepository;
        private TestEntityWithDomainEvent _entity;
        private TestDomainEvent _raisedDomainEvent;

        [SetUp]
        public void Context()
        {
            HttpContext.Current = FakeHttpContextHelper.GetFakeHttpContext();

            var domainEventHandlerFactory = new FakeDomainEventHandlerFactory(domainEvent => _raisedDomainEvent = (TestDomainEvent)domainEvent);
            DomainEvents.Initialize(domainEventHandlerFactory);

            _volatileResourceManager = new VolatileResourceManager();

            var unitOfWorkFactory = IoC.Resolve<IUnitOfWorkFactory>();
            TransactionScopeUnitOfWorkHttpModule.Initialize(
                unitOfWorkFactory: unitOfWorkFactory,
                transactionScopeEnlistmentAction: transactionScope => _volatileResourceManager.EnlistIntoTransactionScope(transactionScope)
                );
            var transactionScopeUnitOfWorkHttpModule = new TransactionScopeUnitOfWorkHttpModule();
            var httpApplication = new FakeHttpApplication();
            transactionScopeUnitOfWorkHttpModule.Init(httpApplication);

            httpApplication.FireBeginRequest();

            _simulateApplicationTransaction();

            httpApplication.FireEndRequest();
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

        [TearDown]
        public void TearDown()
        {
            HttpContext.Current = null;
        }
    }
}
