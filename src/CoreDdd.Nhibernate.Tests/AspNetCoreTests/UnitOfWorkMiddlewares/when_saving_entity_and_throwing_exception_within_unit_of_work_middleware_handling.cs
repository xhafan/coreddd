#if NETCOREAPP
using System;
using System.Threading.Tasks;
using CoreDdd.AspNetCore.Middleware;
using CoreDdd.Domain.Events;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.Tests.TestEntities;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.UnitOfWorks;
using CoreIoC;
using CoreUtils.Storages;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.AspNetCoreTests.UnitOfWorkMiddlewares
{
    [TestFixture]
    public class when_saving_entity_and_throwing_exception_within_unit_of_work_middleware_handling
    {
        private IRepository<TestEntityWithDomainEvent> _entityRepository;
        private TestEntityWithDomainEvent _entity;

        [SetUp]
        public async Task Context()
        {
            var domainEventHandlerFactory = IoC.Resolve<IDomainEventHandlerFactory>();
            var storageFactory = IoC.Resolve<IStorageFactory>();
            DomainEvents.InitializeWithDelayedDomainEventHandling(domainEventHandlerFactory, storageFactory);
            _resetDelayedDomainEventHandlingItemsStorage();
            TestDomainEventHandler.ResetDomainEventWasHandledFlag(); // todo: move this domain event initializion code into a helper class

            var unitOfWorkFactory = IoC.Resolve<IUnitOfWorkFactory>();
            var httpContext = new DefaultHttpContext();
            var transactionScopeUnitOfWorkMiddleware = new UnitOfWorkMiddleware(unitOfWorkFactory);

            try
            {
                await transactionScopeUnitOfWorkMiddleware.InvokeAsync(httpContext, async context =>
                {
                    _entityRepository = IoC.Resolve<IRepository<TestEntityWithDomainEvent>>();
                    _entity = new TestEntityWithDomainEvent();
                    await _entityRepository.SaveAsync(_entity);

                    throw new NotSupportedException("test exception");
                });
            }
            catch (NotSupportedException) {}

            void _resetDelayedDomainEventHandlingItemsStorage()
            {
                IoC.Resolve<IStorage<DelayedDomainEventHandlingItems>>().Set(null);
            }
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
        public void domain_event_is_not_handled()
        {
            TestDomainEventHandler.DomainEventWasHandled.ShouldBeFalse();
        }
    }
}
#endif