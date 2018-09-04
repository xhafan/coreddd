#if NETCOREAPP
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
    [TestFixture(TypeArgs = new[] { typeof (TransactionScopeUnitOfWorkMiddlewareSpecification) })]
    [TestFixture(TypeArgs = new[] { typeof (TransactionScopeUnitOfWorkMicrosoftDependencyInjectionMiddlewareSpecification) })]
    public class when_saving_entity_within_transaction_scope_unit_of_work<TTransactionScopeUnitOfWorkMiddlewareSpecification>
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
            }

            await specification.CreateMiddlewareAndInvokeHandling(_requestDelegate, _volatileResourceManager);
        }

        [Test]
        public async Task entity_is_persisted_and_can_be_fetched_after_the_transaction_commit()
        {
            _entity.ShouldNotBeNull();

            var unitOfWork = IoC.Resolve<NhibernateUnitOfWork>();
            unitOfWork.BeginTransaction();

            _entityRepository = IoC.Resolve<IRepository<TestEntityWithDomainEvent>>();
            _entity = await _entityRepository.GetAsync(_entity.Id);

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