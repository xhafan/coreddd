#if NETCOREAPP
using System;
using System.Threading.Tasks;
using CoreDdd.Domain.Events;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.Tests.TestEntities;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.TestHelpers.DomainEvents;
using CoreIoC;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.Webs.AspNetCoreTests.TransactionScopeUnitOfWorkMiddlewares
{
    [TestFixture(TypeArgs = new[] { typeof(TransactionScopeUnitOfWorkMiddlewareSpecification) })]
    [TestFixture(TypeArgs = new[] { typeof(TransactionScopeUnitOfWorkMicrosoftDependencyInjectionMiddlewareSpecification) })]
    public class when_saving_entity_and_throwing_exception_within_transaction_scope_unit_of_work<TTransactionScopeUnitOfWorkMiddlewareSpecification>
        where TTransactionScopeUnitOfWorkMiddlewareSpecification : ITransactionScopeUnitOfWorkMiddlewareSpecification, new()
    {
        private VolatileResourceManager _volatileResourceManager;
        private IRepository<TestEntityWithDomainEvent> _entityRepository;
        private TestEntityWithDomainEvent _entity;
        private TestDomainEvent _raisedDomainEvent;

        [SetUp]
        public async Task Context()
        {
            var specification = new TTransactionScopeUnitOfWorkMiddlewareSpecification();

            var domainEventHandlerFactory = new FakeDomainEventHandlerFactory(domainEvent => _raisedDomainEvent = (TestDomainEvent)domainEvent);
            DomainEvents.Initialize(domainEventHandlerFactory);

            _volatileResourceManager = new VolatileResourceManager();

            async Task _requestDelegate(HttpContext context)
            {
                _entityRepository = IoC.Resolve<IRepository<TestEntityWithDomainEvent>>();

                _entity = new TestEntityWithDomainEvent();
                _entity.BehaviouralMethodWithRaisingDomainEvent();

                await _entityRepository.SaveAsync(_entity);

                _volatileResourceManager.SetMemberValue(23);

                throw new NotSupportedException("test exception");
            }

            try
            {
                await specification.CreateMiddlewareAndInvokeHandling(_requestDelegate, _volatileResourceManager);
            }
            catch (NotSupportedException) {}
        }

        [Test]
        public async Task entity_is_not_persisted_and_cannot_be_fetched_after_the_transaction_rollback()
        {
            _entity.ShouldNotBeNull();

            var unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
            unitOfWork.BeginTransaction();

            _entityRepository = IoC.Resolve<IRepository<TestEntityWithDomainEvent>>();
            _entity = await _entityRepository.GetAsync(_entity.Id);

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