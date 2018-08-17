#if NETCOREAPP
using System.Threading.Tasks;
using CoreDdd.AspNetCore.Middleware;
using CoreDdd.Domain.Events;
using CoreDdd.Domain.Repositories;
using CoreDdd.Nhibernate.Tests.TestEntities;
using CoreDdd.Nhibernate.UnitOfWorks;
using CoreDdd.UnitOfWorks;
using CoreIoC;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using Shouldly;

namespace CoreDdd.Nhibernate.Tests.AspNetCoreTests.TransactionScopeUnitOfWorkMiddlewares
{
    [TestFixture]
    public class when_saving_entity_within_transaction_scope_unit_of_work_middleware_handling
    {
        private VolatileResourceManager _volatileResourceManager;
        private IRepository<TestEntityWithDomainEvent> _entityRepository;
        private TestEntityWithDomainEvent _entity;

        [SetUp]
        public async Task Context()
        {
            var domainEventHandlerFactory = IoC.Resolve<IDomainEventHandlerFactory>();
            DomainEvents.Initialize(domainEventHandlerFactory);
            TestDomainEventHandler.ResetDomainEventWasHandledFlag(); // todo: move this domain event initializion code into a helper class

            _volatileResourceManager = new VolatileResourceManager();

            var unitOfWorkFactory = IoC.Resolve<IUnitOfWorkFactory>();
            var transactionScopeUnitOfWorkMiddleware = new TransactionScopeUnitOfWorkMiddleware(
                unitOfWorkFactory: unitOfWorkFactory,
                transactionScopeEnlistmentAction: transactionScope => _volatileResourceManager.EnlistIntoTransactionScope(transactionScope)
                );

            var httpContext = new DefaultHttpContext();

            await transactionScopeUnitOfWorkMiddleware.InvokeAsync(httpContext, async context =>
            {
                _entityRepository = IoC.Resolve<IRepository<TestEntityWithDomainEvent>>();

                _entity = new TestEntityWithDomainEvent();
                _entity.BehaviouralMethodWithRaisingDomainEvent();

                await _entityRepository.SaveAsync(_entity);

                _volatileResourceManager.SetMemberValue(23);
            });
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
            TestDomainEventHandler.DomainEventWasHandled.ShouldBeTrue();
        }
    }
}
#endif