#if !NETCOREAPP
using System;
using System.Web;
using CoreDdd.AspNet.HttpModules;
using CoreDdd.Domain.Events;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.Tests.TestEntities;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.TestHelpers.DomainEvents;
using CoreDdd.UnitOfWorks;
using CoreIoC;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Webs.AspNetTests.TransactionScopeUnitOfWorkHttpModules
{
    [TestFixture]
    public class when_saving_entity_and_throwing_exception_within_transaction_scope_unit_of_work_http_module_handling
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

            try
            {
                _simulateApplicationTransactionWhichThrowsAnException();
            }
            catch
            {
                httpApplication.FireError();
            }
        }

        private void _simulateApplicationTransactionWhichThrowsAnException()
        {
            _entityRepository = IoC.Resolve<IRepository<TestEntityWithDomainEvent>>();

            _entity = new TestEntityWithDomainEvent();
            _entity.BehaviouralMethodWithRaisingDomainEvent();

            _entityRepository.Save(_entity);

            _volatileResourceManager.SetMemberValue(23);

            throw new NotSupportedException("test exception");
        }

        [Test]
        public void entity_is_not_persisted_and_cannot_be_fetched_after_the_transaction_rollback()
        {
            _entity.ShouldNotBeNull();

            var unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
            unitOfWork.BeginTransaction();

            _entityRepository = IoC.Resolve<IRepository<TestEntityWithDomainEvent>>();
            _entity = _entityRepository.Get(_entity.Id);

            _entity.ShouldBeNull();

            unitOfWork.Rollback();
        }

        [Test]
        public void volatile_resource_manager_value_is_not_set_after_transaction_scope_rollback()
        {
            _volatileResourceManager.MemberValue.ShouldNotBe(23);
        }

        [Test]
        public void domain_event_is_still_handled_even_though_the_transaction_is_rolled_back()
        {
            // the domain event handler is executed, but developer needs to make sure that domain event handler execution results 
            // are rolled back if the transaction scope is rolled back. For instance when sending a bus message from the domain event handler
            // the message should not be sent when the transaction scope rolls back.
            _raisedDomainEvent.ShouldNotBeNull();
        }
    }
}
#endif