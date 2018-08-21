#if NETCOREAPP
using System.Threading.Tasks;
using CoreDdd.AspNetCore.Middleware;
using CoreDdd.Domain.Events;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.Tests.TestEntities;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.TestHelpers.DomainEvents;
using CoreDdd.UnitOfWorks;
using CoreIoC;
using CoreUtils.Storages;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.AspNetCoreTests.UnitOfWorkMiddlewares
{
    [TestFixture]
    public class when_saving_entity_within_unit_of_work_middleware_handling
    {
        private IRepository<TestEntityWithDomainEvent> _entityRepository;
        private TestEntityWithDomainEvent _entity;
        private TestDomainEvent _raisedDomainEvent;

        [SetUp]
        public async Task Context()
        {
            var domainEventHandlerFactory = new FakeDomainEventHandlerFactory(domainEvent => _raisedDomainEvent = (TestDomainEvent)domainEvent);
            var storageFactory = IoC.Resolve<IStorageFactory>();
            DomainEvents.InitializeWithDelayedDomainEventHandling(domainEventHandlerFactory, storageFactory);
            _resetDelayedDomainEventHandlingItemsStorage();

            var unitOfWorkFactory = IoC.Resolve<IUnitOfWorkFactory>();
            var transactionScopeUnitOfWorkMiddleware = new UnitOfWorkMiddleware(unitOfWorkFactory);

            var httpContext = new DefaultHttpContext();

            await transactionScopeUnitOfWorkMiddleware.InvokeAsync(httpContext, async context =>
            {
                _entityRepository = IoC.Resolve<IRepository<TestEntityWithDomainEvent>>();

                _entity = new TestEntityWithDomainEvent();
                _entity.BehaviouralMethodWithRaisingDomainEvent();

                await _entityRepository.SaveAsync(_entity);
            });

            void _resetDelayedDomainEventHandlingItemsStorage()
            {
                IoC.Resolve<IStorage<DelayedDomainEventHandlingItems>>().Set(null);
            }
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
        public void domain_event_is_handled()
        {
            _raisedDomainEvent.ShouldNotBeNull();
        }
    }
}
#endif